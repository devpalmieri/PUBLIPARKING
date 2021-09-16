#if TODO_COMPLETA_SE_SERVE
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utilities
{
    public class FileWithMaxNumberOfRowsWriter : IDisposable
    {
        public class Parameters
        {
            public Func<string> GetFilePath { get; set; }
            public Func<string> GetHeadRecord { get; set; }
            public Func<string> GetFootRecord { get; set; }
        }

        private int _lineWritten = 0;
        private StreamWriter _sw;

        private readonly Parameters _params;
        private readonly int _maxLineCount;

        private void Begin()
        {
            if (_sw != null)
            {
                End();
            }

            _sw = new StreamWriter(_params.GetFilePath());
            _sw.WriteLine(_params.GetHeadRecord());
        }

        private void End()
        {
            _sw.WriteLine(_params.GetFootRecord());
            _sw.Close();
            _sw.Dispose();
        }

        public FileWithMaxNumberOfRowsWriter(
            int maxLineCount,
            Parameters parameters)
        {
            _params = parameters;
            _maxLineCount = maxLineCount;

            _sw = null;
            Begin();
        }

        public void WriteLine(string lineToWrite)
        {
            _sw.WriteLine(lineToWrite);
            ++_lineWritten;
            if (_lineWritten == _maxLineCount)
            {
                End();
                Begin();
            }
        }

        public void Dispose()
        {
            End();
        }
    }
}
#endif
