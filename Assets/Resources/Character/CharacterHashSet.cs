using System.Collections.Generic;

namespace Resources.Character
{
    public static class CharacterHashSet
    {
        private static readonly Dictionary<string, CharaData.IdentCharacter> CharacterDictionary =
            new Dictionary<string, CharaData.IdentCharacter>()
            {
                { "Bird", CharaData.IdentCharacter.Bird },
                { "Deer", CharaData.IdentCharacter.Deer },
                { "Fish", CharaData.IdentCharacter.Fish },
                { "Gecho", CharaData.IdentCharacter.Gecko },
                { "Monkey", CharaData.IdentCharacter.Monkey },
                { "Mouse", CharaData.IdentCharacter.Mouse },
                { "Snake", CharaData.IdentCharacter.Snake },
                { "Squid", CharaData.IdentCharacter.Squid },
            };

        public static CharaData.IdentCharacter GetCharacterByString(string animal)
        {
            return CharacterDictionary[animal];
        }
    }
}