#if UNITY_EDITOR || FBXSDK_RUNTIME
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Autodesk.Fbx {

public static class FbxPropertyFlags { 
  // virtual void Dispose()  { } 

  public enum EInheritType {
    eOverride,
    eInherit,
    eDeleted
  }

  public enum EFlags {
    eNone = 0,
    eStatic = 1 << 0,
    eAnimatable = 1 << 1,
    eAnimated = 1 << 2,
    eImported = 1 << 3,
    eUserDefined = 1 << 4,
    eHidden = 1 << 5,
    eNotSavable = 1 << 6,
    eLockedMember0 = 1 << 7,
    eLockedMember1 = 1 << 8,
    eLockedMember2 = 1 << 9,
    eLockedMember3 = 1 << 10,
    eLockedAll = eLockedMember0|eLockedMember1|eLockedMember2|eLockedMember3,
    eMutedMember0 = 1 << 11,
    eMutedMember1 = 1 << 12,
    eMutedMember2 = 1 << 13,
    eMutedMember3 = 1 << 14,
    eMutedAll = eMutedMember0|eMutedMember1|eMutedMember2|eMutedMember3,
    eUIDisabled = 1 << 15,
    eUIGroup = 1 << 16,
    eUIBoolGroup = 1 << 17,
    eUIExpanded = 1 << 18,
    eUINoCaption = 1 << 19,
    eUIPanel = 1 << 20,
    eUILeftLabel = 1 << 21,
    eUIHidden = 1 << 22,
    eCtrlFlags = eStatic|eAnimatable|eAnimated|eImported|eUserDefined|eHidden|eNotSavable|eLockedAll|eMutedAll,
    eUIFlags = eUIDisabled|eUIGroup|eUIBoolGroup|eUIExpanded|eUINoCaption|eUIPanel|eUILeftLabel|eUIHidden,
    eAllFlags = eCtrlFlags|eUIFlags,
    eFlagCount = 23
  }

}

}

#endif // UNITY_EDITOR || FBXSDK_RUNTIME