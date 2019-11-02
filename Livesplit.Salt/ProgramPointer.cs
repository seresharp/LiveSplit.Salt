using System;
using System.Diagnostics;

namespace LiveSplit.Salt
{
    public class ProgramPointer
    {
        private readonly string _signature;
        private readonly int _offset;
        private readonly int _derefCount;

        private IntPtr _ptr;
        private int _id = -1;

        private DateTime _nextTry = DateTime.MinValue;

        public ProgramPointer(string signature, int offset, int derefCount)
        {
            if (string.IsNullOrWhiteSpace(signature))
            {
                throw new ArgumentNullException(nameof(signature));
            }

            _signature = signature;
            _offset = offset;
            _derefCount = derefCount;
        }

        public T Read<T>(Process program, params int[] offsets) where T : struct
        {
            GetPointer(program);
            return _ptr == IntPtr.Zero ? default : program.Read<T>(_ptr, offsets);
        }

        private void GetPointer(Process program)
        {
            // Check for program changes to know to re-obtain pointer
            if (program == null)
            {
                _id = -1;
                _ptr = IntPtr.Zero;
                return;
            }

            if (program.Id != _id)
            {
                _id = program.Id;
                _ptr = IntPtr.Zero;
            }

            // Check if pointer get should be retried
            if (_ptr != IntPtr.Zero || DateTime.Now < _nextTry)
            {
                return;
            }

            _nextTry = DateTime.Now.AddSeconds(1);

            // Find signature
            MemorySearcher searcher = new MemorySearcher
            {
                MemoryFilter = info =>
                    (info.State & 0x1000) != 0 && (info.Protect & 0x40) != 0 && (info.Protect & 0x100) == 0
            };

            _ptr = searcher.FindSignature(program, _signature);

            if (_ptr == IntPtr.Zero)
            {
                return;
            }

            // Add offset, dereference
            _ptr += _offset;

            for (int i = 0; i < _derefCount; i++)
            {
                _ptr = (IntPtr) program.Read<uint>(_ptr);
            }
        }
    }
}
