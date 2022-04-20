using System.Collections.Generic;

namespace Map
{
    public class Level
    {
        public bool IsComplited;
        public bool LeftStars;
        public bool RightStars;

        public Level()
        {
            IsComplited = false;
            LeftStars = false;
            RightStars = false;
        }
    }
}


/*
    public class Characters
    {
        public bool CharacterJust;
        public bool CharacterMagmax;
        public bool CharacterMorfe;
        public bool CharacterNecro;
        public bool CharacterNeo;
        public bool CharacterNexus;

        public Characters()
        {
            CharacterJust = true;
            CharacterMagmax = false;
            CharacterMorfe = false;
            CharacterNecro = false;
            CharacterNeo = false;
            CharacterNexus = false;
        }
    }
    public class Company
    {
        public List<Level> Levels;

        public Company(int quantityLevels)
        {
            Levels = new List<Level>();
            for(int i = 0; i < quantityLevels; i++)
            {
                Levels.Add(new Level());
            }
        }
    }
    public class Survival
    {
        public float TimeRecord;

        public Survival()
        {
            TimeRecord = 0;
        }
    }
    public class Setings
    {
        public int VolumeEffect;
        public int VolumeMusic;
        public string Language;
        public string LocationInterface;
        
        public Setings()
        {
            VolumeEffect = 100;
            VolumeMusic = 100;
            Language = "eng";
            LocationInterface = "right";
        }
    }
    public class DataOfProgressPlayer
    {
        public int QuantityCoins;
        public Characters Characters;
        public Company Company;
        public Survival Survival;
        public Setings Setings;

        public DataOfProgressPlayer(int quantityLevels)
        {
            QuantityCoins = 0;
            Characters = new Characters();
            Company = new Company(quantityLevels);
            Survival = new Survival();
            Setings = new Setings();
        }
    }
    */