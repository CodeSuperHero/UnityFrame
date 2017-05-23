//二进制流读取操作类
//CodeSuperHero 20160512
using System.IO;

namespace CodeSuperHero.UF
{
	public class BinaryStream
	{
		private MemoryStream _stream;
		private BinaryReader _reader;
		private BinaryWriter _writer;

		public BinaryStream ()
		{
			_stream = new MemoryStream ();
			_writer = new BinaryWriter (_stream);
		}

		public BinaryStream (byte[] bytes)
		{
			if (bytes == null) {
				_stream = new MemoryStream ();
				_writer = new BinaryWriter (_stream);
			} else {
				_stream = new MemoryStream (bytes);
				_reader = new BinaryReader (_stream);
			}
		}



	}
}