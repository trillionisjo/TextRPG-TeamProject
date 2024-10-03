using System;
using NAudio.Wave;

enum AudioName
{
    sfxEquip,
    sfxUnequip,
    sfxConsumePotion,
    sfxPurchaseItem,
    sfxSellItem,
    sfxUnlocking,
    sfxOpenChest,
    sfxKey,

    voiceNotEnoughtGold,
    voiceNotEnoughKey,
}

class AudioInfo
{
    public Mp3FileReader mp3reader;
    public WaveOutEvent outEvent;
}

class AudioManager
{
    private static WaveOutEvent outputDevice;
    private static LoopStream loopStream;
    private static string currentAudioFilePath;

    private static Dictionary<AudioName, AudioInfo> audios;

    static AudioManager ()
    {
        EquipManager.ItemEquippted += OnItemEquipped;
        EquipManager.ItemUnequippted += OnItemUnequippted;
        Inventory.PotionConsumed += OnPotionConsumed;
        ShopData.Purchased += OnPurchased;
        ShopData.Sold += OnSold;
        DungeonManager.BattleSystem.OnAttack +=OnAttack;
        if (IsWindowsPlatform())
        {
            audios = new Dictionary<AudioName, AudioInfo>();
            foreach (AudioName fileName in Enum.GetValues(typeof(AudioName)))
            {
                string path = $"sfx\\{fileName.ToString()}.mp3";
                var mp3reader = new Mp3FileReader(path);

                audios[fileName] = new AudioInfo();
                audios[fileName].mp3reader = mp3reader;
                audios[fileName].outEvent = new WaveOutEvent();
                audios[fileName].outEvent.Init(mp3reader);
            }
        }
    }

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
        outputDevice.Volume = 0.05f;
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

    // deprecated!!
    public static void PlayOntShot(string filePath)
    {
        if (!IsWindowsPlatform()) return;

        var audioFile = new AudioFileReader(filePath);
        var output = new WaveOutEvent();
        output.Init(audioFile);
        output.Play();

        // 재생이 끝나면 리소스를 정리
        output.PlaybackStopped += (sender, args) =>
        {
            output.Dispose();
            audioFile.Dispose();
        };
    }

    public static void PlayOneShot(AudioName name)
    {
        if (!IsWindowsPlatform())
            return;

        audios[name].mp3reader.Position = 0;
        audios[name].outEvent.Play();
    }

    private static bool IsWindowsPlatform()
    {
        return Environment.OSVersion.Platform == PlatformID.Win32NT;
    }

    private static void OnItemEquipped(Slot slot, IEquipable equipable)
    {
        switch (equipable.Slot)
        {
        case Slot.Body:
            PlayOneShot(AudioName.sfxEquip);
            break;

        case Slot.Hand:
            PlayOneShot(AudioName.sfxEquip);
            break;
        }
    }
    
    private static void OnAttack(AttackType type)
    {
        switch (type)
        {
            case AttackType.Normal:
                PlayOntShot("hit.mp3");
                break;

            case AttackType.Critical:
                PlayOntShot("critical.mp3");
                break;
          
            case AttackType.Miss:
                PlayOntShot("miss.wav");
                break;
        }
    }
    
    private static void OnItemUnequippted(Slot slot)
    {
        switch (slot)
        {
        case Slot.Body:
            PlayOneShot(AudioName.sfxUnequip);
            break;

        case Slot.Hand:
            PlayOneShot(AudioName.sfxUnequip);
            break;
        }
    }

    private static void OnPotionConsumed()
    {
        PlayOneShot(AudioName.sfxConsumePotion);
    }

    private static void OnPurchased()
    {
        PlayOneShot(AudioName.sfxPurchaseItem);
    }

    private static void OnSold()
    {
        PlayOneShot(AudioName.sfxSellItem);
    }
}