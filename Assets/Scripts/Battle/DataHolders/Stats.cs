namespace Battle.DataHolders
{
    public class Stats
    {
        public float mana = 100.0f;
        public float movementSpeed = 1.0f;
        public float strength = 10.0f;
        public float vitality = 30.0f;
        public float health = 100.0f;
        public float manaRegen => strength / 0.5f;
    }
}