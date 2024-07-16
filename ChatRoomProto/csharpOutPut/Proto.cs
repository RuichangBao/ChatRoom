// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Proto.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Proto {

  /// <summary>Holder for reflection information generated from Proto.proto</summary>
  public static partial class ProtoReflection {

    #region Descriptor
    /// <summary>File descriptor for Proto.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtoReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtQcm90by5wcm90bxIFUHJvdG8aDU1zZ1R5cGUucHJvdG8iVwoLVGVzdFJl",
            "cXVlc3QSHwoHbXNnVHlwZRgBIAEoDjIOLlByb3RvLk1zZ1R5cGUSCwoDc3Ry",
            "GAIgASgJEgwKBG51bTEYAyABKAUSDAoEbnVtMhgEIAEoBSJZCg1UZXN0UmVz",
            "cG9uc2VzEh8KB21zZ1R5cGUYASABKA4yDi5Qcm90by5Nc2dUeXBlEgsKA3N0",
            "chgCIAEoCRIMCgRudW0xGAMgASgFEgwKBG51bTIYBCABKAUiRQoSTGlua1N1",
            "Y2Nlc1Jlc3BvbnNlEh8KB21zZ1R5cGUYASABKA4yDi5Qcm90by5Nc2dUeXBl",
            "Eg4KBnVzZXJJZBgCIAEoA2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Proto.MsgTypeReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Proto.TestRequest), global::Proto.TestRequest.Parser, new[]{ "MsgType", "Str", "Num1", "Num2" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Proto.TestResponses), global::Proto.TestResponses.Parser, new[]{ "MsgType", "Str", "Num1", "Num2" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Proto.LinkSuccesResponse), global::Proto.LinkSuccesResponse.Parser, new[]{ "MsgType", "UserId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// 定义一个消息，名为TestRequest
  /// </summary>
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class TestRequest : pb::IMessage<TestRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<TestRequest> _parser = new pb::MessageParser<TestRequest>(() => new TestRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<TestRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Proto.ProtoReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestRequest(TestRequest other) : this() {
      msgType_ = other.msgType_;
      str_ = other.str_;
      num1_ = other.num1_;
      num2_ = other.num2_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestRequest Clone() {
      return new TestRequest(this);
    }

    /// <summary>Field number for the "msgType" field.</summary>
    public const int MsgTypeFieldNumber = 1;
    private global::Proto.MsgType msgType_ = global::Proto.MsgType.EnTestRequest;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Proto.MsgType MsgType {
      get { return msgType_; }
      set {
        msgType_ = value;
      }
    }

    /// <summary>Field number for the "str" field.</summary>
    public const int StrFieldNumber = 2;
    private string str_ = "";
    /// <summary>
    /// 查询字符串
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Str {
      get { return str_; }
      set {
        str_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "num1" field.</summary>
    public const int Num1FieldNumber = 3;
    private int num1_;
    /// <summary>
    /// 页码
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Num1 {
      get { return num1_; }
      set {
        num1_ = value;
      }
    }

    /// <summary>Field number for the "num2" field.</summary>
    public const int Num2FieldNumber = 4;
    private int num2_;
    /// <summary>
    /// 每页结果数
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Num2 {
      get { return num2_; }
      set {
        num2_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as TestRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TestRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MsgType != other.MsgType) return false;
      if (Str != other.Str) return false;
      if (Num1 != other.Num1) return false;
      if (Num2 != other.Num2) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (MsgType != global::Proto.MsgType.EnTestRequest) hash ^= MsgType.GetHashCode();
      if (Str.Length != 0) hash ^= Str.GetHashCode();
      if (Num1 != 0) hash ^= Num1.GetHashCode();
      if (Num2 != 0) hash ^= Num2.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (Str.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Str);
      }
      if (Num1 != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Num1);
      }
      if (Num2 != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Num2);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (Str.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Str);
      }
      if (Num1 != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Num1);
      }
      if (Num2 != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Num2);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgType);
      }
      if (Str.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Str);
      }
      if (Num1 != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Num1);
      }
      if (Num2 != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Num2);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TestRequest other) {
      if (other == null) {
        return;
      }
      if (other.MsgType != global::Proto.MsgType.EnTestRequest) {
        MsgType = other.MsgType;
      }
      if (other.Str.Length != 0) {
        Str = other.Str;
      }
      if (other.Num1 != 0) {
        Num1 = other.Num1;
      }
      if (other.Num2 != 0) {
        Num2 = other.Num2;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 18: {
            Str = input.ReadString();
            break;
          }
          case 24: {
            Num1 = input.ReadInt32();
            break;
          }
          case 32: {
            Num2 = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 18: {
            Str = input.ReadString();
            break;
          }
          case 24: {
            Num1 = input.ReadInt32();
            break;
          }
          case 32: {
            Num2 = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  /// 定义一个消息，名为TestResponses
  /// </summary>
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class TestResponses : pb::IMessage<TestResponses>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<TestResponses> _parser = new pb::MessageParser<TestResponses>(() => new TestResponses());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<TestResponses> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Proto.ProtoReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestResponses() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestResponses(TestResponses other) : this() {
      msgType_ = other.msgType_;
      str_ = other.str_;
      num1_ = other.num1_;
      num2_ = other.num2_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public TestResponses Clone() {
      return new TestResponses(this);
    }

    /// <summary>Field number for the "msgType" field.</summary>
    public const int MsgTypeFieldNumber = 1;
    private global::Proto.MsgType msgType_ = global::Proto.MsgType.EnTestRequest;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Proto.MsgType MsgType {
      get { return msgType_; }
      set {
        msgType_ = value;
      }
    }

    /// <summary>Field number for the "str" field.</summary>
    public const int StrFieldNumber = 2;
    private string str_ = "";
    /// <summary>
    /// 查询字符串
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Str {
      get { return str_; }
      set {
        str_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "num1" field.</summary>
    public const int Num1FieldNumber = 3;
    private int num1_;
    /// <summary>
    /// 页码
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Num1 {
      get { return num1_; }
      set {
        num1_ = value;
      }
    }

    /// <summary>Field number for the "num2" field.</summary>
    public const int Num2FieldNumber = 4;
    private int num2_;
    /// <summary>
    /// 每页结果数
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Num2 {
      get { return num2_; }
      set {
        num2_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as TestResponses);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(TestResponses other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MsgType != other.MsgType) return false;
      if (Str != other.Str) return false;
      if (Num1 != other.Num1) return false;
      if (Num2 != other.Num2) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (MsgType != global::Proto.MsgType.EnTestRequest) hash ^= MsgType.GetHashCode();
      if (Str.Length != 0) hash ^= Str.GetHashCode();
      if (Num1 != 0) hash ^= Num1.GetHashCode();
      if (Num2 != 0) hash ^= Num2.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (Str.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Str);
      }
      if (Num1 != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Num1);
      }
      if (Num2 != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Num2);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (Str.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Str);
      }
      if (Num1 != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Num1);
      }
      if (Num2 != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Num2);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgType);
      }
      if (Str.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Str);
      }
      if (Num1 != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Num1);
      }
      if (Num2 != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Num2);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(TestResponses other) {
      if (other == null) {
        return;
      }
      if (other.MsgType != global::Proto.MsgType.EnTestRequest) {
        MsgType = other.MsgType;
      }
      if (other.Str.Length != 0) {
        Str = other.Str;
      }
      if (other.Num1 != 0) {
        Num1 = other.Num1;
      }
      if (other.Num2 != 0) {
        Num2 = other.Num2;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 18: {
            Str = input.ReadString();
            break;
          }
          case 24: {
            Num1 = input.ReadInt32();
            break;
          }
          case 32: {
            Num2 = input.ReadInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 18: {
            Str = input.ReadString();
            break;
          }
          case 24: {
            Num1 = input.ReadInt32();
            break;
          }
          case 32: {
            Num2 = input.ReadInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  /// <summary>
  ///客户端链接成功
  /// </summary>
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class LinkSuccesResponse : pb::IMessage<LinkSuccesResponse>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LinkSuccesResponse> _parser = new pb::MessageParser<LinkSuccesResponse>(() => new LinkSuccesResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<LinkSuccesResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Proto.ProtoReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LinkSuccesResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LinkSuccesResponse(LinkSuccesResponse other) : this() {
      msgType_ = other.msgType_;
      userId_ = other.userId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LinkSuccesResponse Clone() {
      return new LinkSuccesResponse(this);
    }

    /// <summary>Field number for the "msgType" field.</summary>
    public const int MsgTypeFieldNumber = 1;
    private global::Proto.MsgType msgType_ = global::Proto.MsgType.EnTestRequest;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Proto.MsgType MsgType {
      get { return msgType_; }
      set {
        msgType_ = value;
      }
    }

    /// <summary>Field number for the "userId" field.</summary>
    public const int UserIdFieldNumber = 2;
    private long userId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long UserId {
      get { return userId_; }
      set {
        userId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as LinkSuccesResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(LinkSuccesResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MsgType != other.MsgType) return false;
      if (UserId != other.UserId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (MsgType != global::Proto.MsgType.EnTestRequest) hash ^= MsgType.GetHashCode();
      if (UserId != 0L) hash ^= UserId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (UserId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(UserId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (UserId != 0L) {
        output.WriteRawTag(16);
        output.WriteInt64(UserId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (MsgType != global::Proto.MsgType.EnTestRequest) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgType);
      }
      if (UserId != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(UserId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(LinkSuccesResponse other) {
      if (other == null) {
        return;
      }
      if (other.MsgType != global::Proto.MsgType.EnTestRequest) {
        MsgType = other.MsgType;
      }
      if (other.UserId != 0L) {
        UserId = other.UserId;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 16: {
            UserId = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        // Abort on any end group tag.
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            MsgType = (global::Proto.MsgType) input.ReadEnum();
            break;
          }
          case 16: {
            UserId = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code