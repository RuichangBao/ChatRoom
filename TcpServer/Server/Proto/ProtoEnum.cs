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
            "Cg9Qcm90b0VudW0ucHJvdG8SBVByb3RvKt0CCgdNc2dUeXBlEhEKDWVuUmVx",
            "dWVzdFRlc3QQABITCg9lblJlc3BvbnNlc1Rlc3QQARIRCg1lblJlcXVlc3RM",
            "aW5rEGQSEgoOZW5SZXNwb25zZUxpbmsQZRIXChNlblJlcXVlc3RDcmVhdGVS",
            "b29tEGYSGAoUZW5SZXNwb25zZUNyZWF0ZVJvb20QZxIVChFlblJlcXVlc3RK",
            "b2luUm9vbRBoEhYKEmVuUmVzcG9uc2VKb2luUm9vbRBpEhcKE2VuUmVzcG9u",
            "c2VPdGhlckpvaW4QahIWChJlblJlcXVlc3RMZWF2ZVJvb20QaxIXChNlblJl",
            "c3BvbnNlTGVhdmVSb29tEGwSGAoUZW5SZXNwb25zZU90aGVyTGVhdmUQbRIS",
            "Cg1lblJlcXVlc3RTZW5kEMgBEhMKDmVuUmVzcG9uc2VTZW5kEMkBEhQKD2Vu",
            "UmVzcG9uc2VFcnJvchC4FyopCglFcnJvckNvZGUSCwoHRXJyVGVzdBAAEg8K",
            "C0VyclJvb21Ob25lEGRiBnByb3RvMw=="));
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
    [pbr::OriginalName("enRequestLink")] EnRequestLink = 100,
    /// <summary>
    ///客户端链接成功
    /// </summary>
    [pbr::OriginalName("enResponseLink")] EnResponseLink = 101,
    /// <summary>
    ///创建房间
    /// </summary>
    [pbr::OriginalName("enRequestCreateRoom")] EnRequestCreateRoom = 102,
    [pbr::OriginalName("enResponseCreateRoom")] EnResponseCreateRoom = 103,
    /// <summary>
    ///加入房间
    /// </summary>
    [pbr::OriginalName("enRequestJoinRoom")] EnRequestJoinRoom = 104,
    [pbr::OriginalName("enResponseJoinRoom")] EnResponseJoinRoom = 105,
    /// <summary>
    ///有人加入房间
    /// </summary>
    [pbr::OriginalName("enResponseOtherJoin")] EnResponseOtherJoin = 106,
    /// <summary>
    ///退出房间
    /// </summary>
    [pbr::OriginalName("enRequestLeaveRoom")] EnRequestLeaveRoom = 107,
    [pbr::OriginalName("enResponseLeaveRoom")] EnResponseLeaveRoom = 108,
    /// <summary>
    ///有人离开房间
    /// </summary>
    [pbr::OriginalName("enResponseOtherLeave")] EnResponseOtherLeave = 109,
    /// <summary>
    ///客户端发送消息
    /// </summary>
    [pbr::OriginalName("enRequestSend")] EnRequestSend = 200,
    [pbr::OriginalName("enResponseSend")] EnResponseSend = 201,
    /// <summary>
    ///服务器返回错误码
    /// </summary>
    [pbr::OriginalName("enResponseError")] EnResponseError = 3000,
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
