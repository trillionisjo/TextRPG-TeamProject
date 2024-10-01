using System;
using NAudio.Wave;

class AudioManager
{
    private static WaveOutEvent outputDevice;
    private static LoopStream loopStream;
    private static string currentAudioFilePath;

    public static void PlayAudio(string filePath)
    {
        var os = Environment.OSVersion;
        if(os.Platform != PlatformID.Win32NT)return;
        // 현재 재생 중인 오디오와 새로 재생하려는 오디오가 동일한지 확인
        if (currentAudioFilePath == filePath && outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
        {
            return; // 동일한 파일이 재생 중이면 아무 작업도 하지 않음
        }

        // 현재 재생 중인 오디오가 다른 파일이면 정지
        StopAudio();

        // 새로운 오디오 파일 재생 및 반복 설정
        var audioFile = new AudioFileReader(filePath);
        loopStream = new LoopStream(audioFile); // 반복 재생을 위한 LoopStream 사용

        outputDevice = new WaveOutEvent();
        outputDevice.Init(loopStream);
        outputDevice.Play();

        currentAudioFilePath = filePath;
    }

    private static void StopAudio()
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
            outputDevice.Dispose();
        }
        if (loopStream != null)
        {
            loopStream.Dispose();
        }
    }
}