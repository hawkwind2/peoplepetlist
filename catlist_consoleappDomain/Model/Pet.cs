﻿using System;

namespace catlist_consoleapp
{
    public class Pet
    {
        public string Name { get; set; }
        public PetType Type { get; set; }
    }

    public enum PetType
    {
        Cat,
        Dog,
        Fish
    }
}
