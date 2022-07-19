﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exif
{
    public class DataProcessor
    {
        public bool IsLittleEndian { get; set; }
        public bool IsBigEndian { get => !this.IsLittleEndian; set => this.IsLittleEndian = !value; }

        public Stream BaseStream { get; private set; }
        public long ReadLength { get; private set; }
        public long WriteLength { get; private set; }

        public DataProcessor(Stream stream)
        {
            this.IsLittleEndian = BitConverter.IsLittleEndian;
            this.BaseStream = stream;
        }

        public virtual bool CanRead { get { return this.BaseStream.CanRead; } }

        public virtual bool CanWrite { get { return this.BaseStream.CanWrite; } }

        public virtual bool CanSeek { get { return this.BaseStream.CanSeek; } }

        public virtual bool CanTimeout { get { return this.BaseStream.CanTimeout; } }

        public virtual long Position
        {
            get { return this.BaseStream.Position; }
            set { this.BaseStream.Position = value; }
        }

        public virtual bool TryGetPosition(out long position) => this.BaseStream.TryGetPosition(out position);

        public virtual long Length { get { return this.BaseStream.Length; } }

        public virtual bool TryGetLength(out long length) => this.BaseStream.TryGetLength(out length);

        public virtual long Remain { get { return this.BaseStream.GetRemain(); } }

        public virtual bool TryGetRemain(out long remain) => this.BaseStream.TryGetRemain(out remain);

        public virtual void Write(byte[] buffer, int offset, int count)
        {
            this.BaseStream.Write(buffer, offset, count);
            this.WriteLength += count;
        }

        public virtual void WriteByte(byte value)
        {
            this.BaseStream.WriteByte(value);
            this.WriteLength++;
        }

        public virtual void WriteBytes(byte[] value)
        {
            this.Write(value, 0, value.Length);
        }
        public virtual void WriteSByte(sbyte value)
        {
            this.WriteByte((byte)value);
        }

        public virtual void WriteShort(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteUShort(ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteChar(char value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteInt(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteUInt(uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteLong(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteULong(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteFloat(float value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteDouble(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual void WriteGuid(Guid value)
        {
            var bytes = value.ToByteArray();
            this.FlipCheck(bytes);
            this.WriteBytes(bytes);
        }

        public virtual int Read(byte[] buffer, int offset, int count)
        {
            var length = this.BaseStream.Read(buffer, offset, count);
            this.ReadLength += length;
            return length;
        }

        public virtual bool TryReadByte(out byte data)
        {
            var d = this.BaseStream.ReadByte();

            if (d == -1)
            {
                data = 0;
                return false;
            }
            else
            {
                this.ReadLength++;
                data = (byte)d;
                return true;
            }

        }

        public virtual byte ReadByte()
        {
            if (this.TryReadByte(out var data) == true)
            {
                return data;
            }
            else
            {
                throw new IOException();
            }

        }

        public virtual void SkipByRead(long length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(length)} is {length}");
            }

            for (var i = 0L; i < length; i++)
            {
                this.ReadByte();
            }

        }

        public virtual byte[] ReadBytes(long length)
        {
            var bytes = new byte[length];
            this.ReadBytes(bytes);

            return bytes;
        }

        public virtual void ReadBytes(byte[] bytes)
        {
            this.Read(bytes, 0, bytes.Length);
        }

        public virtual sbyte ReadSByte()
        {
            return (sbyte)this.ReadByte();
        }

        public virtual short ReadShort()
        {
            var bytes = this.ReadBytes(2);
            this.FlipCheck(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public virtual ushort ReadUShort()
        {
            var bytes = this.ReadBytes(2);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public virtual int ReadInt()
        {
            var bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public virtual uint ReadUInt()
        {
            var bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public virtual long ReadLong()
        {
            var bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public virtual ulong ReadULong()
        {
            var bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        public virtual float ReadFloat()
        {
            var bytes = this.ReadBytes(4);
            this.FlipCheck(bytes);
            return BitConverter.ToSingle(bytes, 0);
        }

        public virtual double ReadDouble()
        {
            var bytes = this.ReadBytes(8);
            this.FlipCheck(bytes);
            return BitConverter.ToDouble(bytes, 0);
        }

        public virtual Guid ReadGuid()
        {
            var bytes = this.ReadBytes(16);
            this.FlipCheck(bytes);
            return new Guid(bytes);
        }

        protected virtual void FlipCheck(byte[] bytes)
        {
            if (this.IsLittleEndian != BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

        }

    }

}
