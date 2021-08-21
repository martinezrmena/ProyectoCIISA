using System;
using System.Data;
using System.IO;
using System.IO.Compression;

namespace CIISA.RetailOnLine.Droid.Framework.Common.Compress
{
    public class MemoryCompress
    {
        private string GetTempFileName(string pfullFileName)
        {
            return Path.Combine(
                Path.GetDirectoryName(pfullFileName),
                string.Format("TMP{0}.zip", DateTime.Now.Ticks.ToString()
                )
                );
        }

        public void UnzipFromMemoryStream(string pserialized, string pfullFileName)
        {
            string _compressFile = GetTempFileName(pfullFileName);

            byte[] _cache = Convert.FromBase64String(pserialized);

            using (var _file = File.OpenWrite(_compressFile))
            {
                _file.Write(_cache, 0, _cache.Length);
            }

            using (FileStream _inFile = File.OpenRead(_compressFile))
            {
                using (FileStream _outFile = File.Create(pfullFileName))
                {
                    using (GZipStream _decompress = new GZipStream(_inFile, CompressionMode.Decompress))
                    {
                        byte[] _buffer = new byte[4096];

                        int _numRead;

                        while ((_numRead = _decompress.Read(_buffer, 0, _buffer.Length)) != 0)
                        {
                            _outFile.Write(_buffer, 0, _numRead);
                        }
                    }
                }
            }

            File.Delete(_compressFile);
        }

        internal DataSet UnzipFromMemoryStream_DataSet(string pserialized, string pfullFileName)
        {
            UnzipFromMemoryStream(pserialized, pfullFileName);

            DataSet _ds = new DataSet();

            if (File.Exists(pfullFileName))
            {
                using (FileStream _inFile = File.OpenRead(pfullFileName))
                {
                    _ds.ReadXml(_inFile);
                };
            }

            return _ds;
        }

        public string ZipToMemoryStream(string pfullFileName)
        {
            string _compressFile = GetTempFileName(pfullFileName);

            using (FileStream _inFile = File.OpenRead(pfullFileName))
            {
                using (FileStream _outFile = File.Create(_compressFile))
                {
                    using (var _compress = new GZipStream(_outFile, CompressionMode.Compress))
                    {
                        byte[] _buffer = new byte[4096];

                        int _numRead;

                        while ((_numRead = _inFile.Read(_buffer, 0, _buffer.Length)) != 0)
                        {
                            _compress.Write(_buffer, 0, _numRead);
                        }
                    }
                }
            }

            byte[] _cache;

            using (var _stream = File.OpenRead(_compressFile))
            {
                using (var _reader = new BinaryReader(_stream))
                {
                    _cache = _reader.ReadBytes(Convert.ToInt32(_stream.Length));
                }
            }

            File.Delete(_compressFile);

            return Convert.ToBase64String(_cache);
        }

    }
}