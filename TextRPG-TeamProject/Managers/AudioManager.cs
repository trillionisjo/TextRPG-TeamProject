using System;
using NAudio.Wave;

class AudioManager
{
    private static WaveOutEvent outputDevice;
    private static AudioFileReader audioFile;
    private static string currentAudioFilePath;
    public static void PlayAudio(string filePath)
    {
        // 현재 재생 중인 오디오와 새로 재생하려는 오디오가 동일한지 확인
        if (currentAudioFilePath == filePath && outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
        {
            return; // 동일한 파일이 재생 중이면 아무 작업도 하지 않음
        }

        // 현재 재생 중인 오디오가 다른 파일이면 정지
        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
        {
            outputDevice.Stop();
        }

        // 새로운 오디오 파일 재생
        audioFile = new AudioFileReader(filePath);
        outputDevice = new WaveOutEvent();
        outputDevice.Init(audioFile);
        outputDevice.Play();
        currentAudioFilePath = filePath;
    }
}