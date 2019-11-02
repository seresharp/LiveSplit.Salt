namespace LiveSplit.Salt
{
    public class EnemyHealthTracker
    {
        private readonly SaltMemory _mem;

        public int CharIndex { get; }
        public EnemyType CharType { get; }

        public bool Done { get; private set; }
        public bool ShouldSplit { get; private set; }

        public EnemyHealthTracker(SaltMemory mem, int ch)
        {
            if (mem == null)
            {
                Done = true;
                return;
            }

            _mem = mem;
            CharIndex = ch;
            CharType = _mem.GetCharType(ch);
        }

        public void Update()
        {
            if (Done)
            {
                return;
            }

            // Check for enemy unloading
            if (_mem.GetCharType(CharIndex) != CharType)
            {
                Done = true;
                return;
            }

            // Check for enemy dead
            if (_mem.GetCharHealth(CharIndex) <= 0)
            {
                ShouldSplit = true;
                Done = true;
            }
        }
    }
}
