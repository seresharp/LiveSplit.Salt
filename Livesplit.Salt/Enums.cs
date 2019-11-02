// ReSharper disable UnusedMember.Global

namespace LiveSplit.Salt
{
    public enum TransitionMode
    {
        None,
        Out,
        In,
        AllOut,
        AllIn
    }

    public enum MenuLevel
    {
        PressStart,
        Main,
        QuitYouSure,
        CreateCharacter,
        SelectClass,
        SelectSkin,
        SelectHair,
        SelectHairColor,
        SelectEyeColor,
        SelectSkinClass,
        SelectName,
        VentureForth,
        CancelCreate,
        SelectBeard,
        SelectBeardColor,
        CharListLevel,
        NameCharacter,
        Pause,
        EndGameYouSure,
        Options,
        OptionsVideo,
        OptionsAcceptVideo,
        PaxDemoChars,
        MenuNotes,
        Bestiary,
        BestiaryBeast,
        DeleteCharYouSure,
        ReallyDeleteCharYouSure,
        Audio,
        SelectSupplies,
        LanguageOptions,
        ChangeLanguage,
        GameOptions,
        ChallengeOptions,
        Controls,
        KeyMapping,
        RemapKey,
        KeyControls,
        AcceptSameKeyYouSure,
        ResetKeysYouSure,
        TexturePackList
    }

    public enum GameState
    {
        Menu,
        Playing,
        MenuIntro,
        Loading,
        Credits
    }

    public enum EnemyType
    {
        Unknown,
        Leviathon = 32 // [sic]
    }
}
