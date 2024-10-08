// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ProtoEnum.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Proto {

  /// <summary>Holder for reflection information generated from ProtoEnum.proto</summary>
  public static partial class ProtoEnumReflection {

    #region Descriptor
    /// <summary>File descriptor for ProtoEnum.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtoEnumReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9Qcm90b0VudW0ucHJvdG8SBVByb3RvKusBCgdNc2dUeXBlEhEKDWVuUmVx",
            "dWVzdFRlc3QQABITCg9lblJlc3BvbnNlc1Rlc3QQARIRCg1lblJlcXVlc3RM",
            "aW5rEGMSEgoOZW5SZXNwb25zZUxpbmsQZBIXChNlblJlcXVlc3RDcmVhdGVS",
            "b29tEGUSGAoUZW5SZXNwb25zZUNyZWF0ZVJvb20QZhIVChFlblJlcXVlc3RK",
            "b2luUm9vbRBnEhYKEmVuUmVzcG9uc2VKb2luUm9vbRBoEhYKEmVuUmVxdWVz",
            "dExlYXZlUm9vbRBpEhcKE2VuUmVzcG9uc2VMZWF2ZVJvb20QaiopCglFcnJv",
            "ckNvZGUSCwoHRXJyVGVzdBAAEg8KC0VyclJvb21Ob25lEGRiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Proto.MsgType), typeof(global::Proto.ErrorCode), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum MsgType {
    [pbr::OriginalName("enRequestTest")] EnRequestTest = 0,
    [pbr::OriginalName("enResponsesTest")] EnResponsesTest = 1,
    /// <summary>
    ///客户端请求连接
    /// </summary>
    [pbr::OriginalName("enRequestLink")] EnRequestLink = 99,
    /// <summary>
    ///客户端链接成功
    /// </summary>
    [pbr::OriginalName("enResponseLink")] EnResponseLink = 100,
    /// <summary>
    ///创建房间
    /// </summary>
    [pbr::OriginalName("enRequestCreateRoom")] EnRequestCreateRoom = 101,
    [pbr::OriginalName("enResponseCreateRoom")] EnResponseCreateRoom = 102,
    /// <summary>
    ///加入房间
    /// </summary>
    [pbr::OriginalName("enRequestJoinRoom")] EnRequestJoinRoom = 103,
    [pbr::OriginalName("enResponseJoinRoom")] EnResponseJoinRoom = 104,
    /// <summary>
    ///退出房间
    /// </summary>
    [pbr::OriginalName("enRequestLeaveRoom")] EnRequestLeaveRoom = 105,
    [pbr::OriginalName("enResponseLeaveRoom")] EnResponseLeaveRoom = 106,
  }

  public enum ErrorCode {
    [pbr::OriginalName("ErrTest")] ErrTest = 0,
    /// <summary>
    ///房间不存在
    /// </summary>
    [pbr::OriginalName("ErrRoomNone")] ErrRoomNone = 100,
  }

  #endregion

}

#endregion Designer generated code
