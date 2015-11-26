using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Audition
{
    public partial class AudioDetection : Form
    {
        private MMDeviceEnumerator _deviceEnumerator;
        private DeviceItem _nowMicDevice;
        private DeviceItem _nowSpeakerDevice;
        private List<DeviceItem> _micDevices;
        private List<DeviceItem> _speakerDevices;

        private static bool _isStartRecording;

        public AudioDetection()
        {
            InitializeComponent();
            button1.Enabled = false;
            Closing += Form1_Closing;
        }

        void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopRecording();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDevices();

            micphone_selector.DataSource = _micDevices;
            micphone_selector.DisplayMember = "FriendlyName";

            
            speaker_selector.DataSource = _speakerDevices;
            speaker_selector.DisplayMember = "FriendlyName";
            
            timer1.Enabled = true;
        }

        private void LoadDevices()
        {
            _deviceEnumerator = _deviceEnumerator?? new MMDeviceEnumerator();

            var defaultCapture = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            var defaultRender = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

            _micDevices = new List<DeviceItem>();
            foreach (var device in _deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All))
            {
                var item = new DeviceItem
                {
                    Device = device,
                    Default = defaultCapture.ID == device.ID,
                    Index = _micDevices.Count
                };
                _micDevices.Add(item);
            }

            micphone_selector.BeginUpdate();
            _micDevices.Sort((item1, item2) => item1.Default ? -1 : 1);
            micphone_selector.DataSource = _micDevices;
            micphone_selector.EndUpdate();

            _speakerDevices = new List<DeviceItem>();
            foreach (var device in _deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.All))
            {
                var item = new DeviceItem
                {
                    Device = device,
                    Default = defaultRender.ID == device.ID,
                    Index = _speakerDevices.Count
                };
                _speakerDevices.Add(item);
            }

            speaker_selector.BeginUpdate();
            _speakerDevices.Sort((item1, item2) => item1.Default ? -1 : 1);
            speaker_selector.DataSource = _speakerDevices;
            speaker_selector.EndUpdate();
        }

        private void micphone_selector_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isChanged = false;
            if (_nowMicDevice != null)
            {
                isChanged = true;
                _nowMicDevice.Device.AudioEndpointVolume.OnVolumeNotification -= AudioEndpointVolume_OnVolumeNotification;
            }
            _nowMicDevice = _micDevices[micphone_selector.SelectedIndex];
            _nowMicDevice.Device.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
            micphone_trackbar.Value = (int)(_nowMicDevice.Device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);

            Console.WriteLine("Microphone Changed:{0},Name:{1}",micphone_selector.SelectedIndex,_nowMicDevice.FriendlyName);

            if (isChanged)
            {
                SetDefaultAudioEndPoint(_nowMicDevice.Device.ID);
            }

            SwitcherDeviceTrack();
        }
        
        private void speaker_selector_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isChanged = false;
            if (_nowSpeakerDevice != null)
            {
                isChanged = true;
                _nowSpeakerDevice.Device.AudioEndpointVolume.OnVolumeNotification -= SpeakerAudioEndpointVolume_OnVolumeNotification;
            }
            _nowSpeakerDevice = _speakerDevices[speaker_selector.SelectedIndex];
            _nowSpeakerDevice.Device.AudioEndpointVolume.OnVolumeNotification += SpeakerAudioEndpointVolume_OnVolumeNotification;
            speaker_volume.Value = (int)(_nowSpeakerDevice.Device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            Console.WriteLine("Speaker Changed:{0},Name:{1}", speaker_selector.SelectedIndex, _nowSpeakerDevice.FriendlyName);

            if (isChanged)
            {
                SetDefaultAudioEndPoint(_nowSpeakerDevice.Device.ID);
            }

            SwitcherDeviceTrack();
        }

        private void SwitcherDeviceTrack()
        {
            if (_nowMicDevice != null && _nowSpeakerDevice != null)
            {
                button1.Enabled = true;
                
            }
        }

        private void SpeakerAudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[1];
                Params[0] = data;
                this.Invoke(new AudioEndpointVolumeNotificationDelegate(SpeakerAudioEndpointVolume_OnVolumeNotification), Params);
            }
            else
            {
                speaker_volume.Value = (int)(data.MasterVolume * 100);
            }
        }

        

        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[1];
                Params[0] = data;
                this.Invoke(new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification), Params);
            }
            else
            {
                micphone_trackbar.Value = (int)(data.MasterVolume * 100);
            }
        }

        private void micphone_trackbar_Scroll(object sender, EventArgs e)
        {
            if (_nowMicDevice != null)
            {
                _nowMicDevice.Device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)micphone_trackbar.Value / (100 * 1.0f));
            }
        }

        private void speaker_volume_Scroll(object sender, EventArgs e)
        {
            if (_nowSpeakerDevice != null)
            {
                _nowSpeakerDevice.Device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)speaker_volume.Value / 100.0f);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_nowMicDevice != null)
            {
                micphone_peak.Value = (int)(_nowMicDevice.Device.AudioMeterInformation.MasterPeakValue * 100);
            }

            if (_nowSpeakerDevice != null)
            {
                speaker_peak.Value = (int)(_nowSpeakerDevice.Device.AudioMeterInformation.MasterPeakValue * 100);
            }
        }


        /********************************************************************/
        private WaveIn microphone;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut player;


        private void StartRecording()
        {
            StopRecording();
            _isStartRecording = true;
            Console.WriteLine("start recording...");
            // set up the recorder
            microphone = new WaveIn
            {
                DeviceNumber = _nowMicDevice.Index,
                BufferMilliseconds = 100,
                WaveFormat = new WaveFormat(44100, 2)
            };
            microphone.DataAvailable += MicrophoneOnDataAvailable;
            microphone.RecordingStopped += MicrophoneRecordingStopped;

            // set up our signal chain
            bufferedWaveProvider = new BufferedWaveProvider(microphone.WaveFormat) {DiscardOnBufferOverflow = true};
            // set up playback
            player = new WaveOut {/*DeviceNumber = _nowSpeakerDevice.Index*/};
            
            player.Init(bufferedWaveProvider);


            // begin playback & record
            Console.WriteLine("now speaker:{0}",_nowSpeakerDevice.FriendlyName);
            player.Play();
            microphone.StartRecording();
        }

        void MicrophoneRecordingStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                if (microphone != null)
                {
                    microphone.Dispose();
                    microphone = null;    
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
            }
        }

        private void MicrophoneOnDataAvailable(object sender, WaveInEventArgs e)
        {
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void StopRecording()
        {
            _isStartRecording = false;
            Console.WriteLine("stop recording...");
            if (microphone != null)
            {
                // stop recording
                microphone.StopRecording();    
            }

            if (player != null)
            {
                // stop playback
                player.Stop();    
            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isStartRecording)
            {
                StopRecording();
                button1.Text = "开始";
                micphone_selector.Enabled = true;
                speaker_selector.Enabled = true;
            }
            else
            {
                if (_nowMicDevice != null && _nowSpeakerDevice != null) { 
                    StartRecording();
                    button1.Text = "停止";
                }
                micphone_selector.Enabled = false;
                speaker_selector.Enabled = false;
            }
        }

        private void SetDefaultAudioEndPoint(string devId)
        {
            PolicyConfigClient client = new PolicyConfigClient();
            client.SetDefaultEndpoint(devId,ERole.eConsole);
            client.SetDefaultEndpoint(devId,ERole.eMultimedia);
            client.SetDefaultEndpoint(devId, ERole.eCommunications);
            LoadDevices();
        }
    }

    internal class DeviceItem
    {
        public MMDevice Device;
        public bool Default { get; set; }
        public int Index { get; set; }
        public string FriendlyName
        {
            get { return Device.FriendlyName + (Device.State == DeviceState.Active && Default ? " （默认）" : ""); }
        }
    }



}