using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SATSystem
{
  /// <summary>
  /// Length 272
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct nfc_device_desc_t {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string acDevice;
    public IntPtr pcDriver;
    public IntPtr pcPort;
    public uint uiSpeed;
    public uint uiBusIndex;
  }


  /// <summary>
  /// Length 280
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct nfc_device_t {
    //0
    public IntPtr driver_callbacks;
    //4
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string acDevice;
    //260
    public IntPtr nc;
    //264
    public IntPtr nds;
    //268
    public byte bActive;
    //272
    public byte bCrc;
    //272
    public byte bPar;
    //272
    public byte bEasyFraming;
    //272
    public byte ui8TxBits;
    //276
    public int iLastError;
    //280
  }

  /// <summary>
  /// Length 13
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct nfc_dep_info_t {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] NFCID3i;
    public byte btDID;
    public byte btBSt;
    public byte btBRt;
  }

  /// <summary>
  /// Length = 57
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct nfc_iso14443a_info_t {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[] abtAtqa;
    public byte btSak;
    public uint szUidLen;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public byte[] abtUid;
    public uint szAtsLen;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
    public byte[] abtAts;
  }

  /// <summary>
  /// Length = 23
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct nfc_felica_info_t {
    public uint szLen;
    public byte btResCode;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte abtId;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    public byte abtPad;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte abtSysCode;
  }

  /// <summary>
  /// Length = 89
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct nfc_iso14443b_info_t {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
    public byte[] abtAtqb;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] abtId;
    public byte btParam1;
    public byte btParam2;
    public byte btParam3;
    public byte btParam4;
    public byte btCid;
    public uint szInfLen;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    public byte[] abtInf;
  }

  /// <summary>
  /// Length = 6
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct nfc_jewel_info_t {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte btSensRes;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte btId;
  }

  /// <summary>
  /// Length = 89
  /// </summary>
  [StructLayout(LayoutKind.Explicit, Pack = 1)]
  public struct nfc_target_info_t {
    [FieldOffset(0)]
    public nfc_iso14443a_info_t nai;
/*    [FieldOffset(0)]
    public nfc_felica_info_t nfi;
    [FieldOffset(0)]
    public nfc_iso14443b_info_t nbi;
    [FieldOffset(0)]
    public nfc_jewel_info_t nji;
    [FieldOffset(0)]
    public nfc_dep_info_t ndi;*/
  }

  /// <summary>
  /// Length = 93
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct nfc_target_t {
    public nfc_target_info_t nti;
    public LibNFC.nfc_target_type_t ntt;
  }

  public class LibNFC {
    public enum nfc_chip_t {
      NC_PN531 = 0x10,
      NC_PN532 = 0x20,
      NC_PN533 = 0x30
    }
    public enum nfc_device_option_t {
      /** Let the PN53X chip handle the CRC bytes. This means that the chip appends the CRC bytes to the frames that are transmitted. It will parse the last bytes from received frames as incoming CRC bytes. They will be verified against the used modulation and protocol. If an frame is expected with incorrect CRC bytes this option should be disabled. Example frames where this is useful are the ATQA and UID+BCC that are transmitted without CRC bytes during the anti-collision phase of the ISO14443-A protocol. */
      NDO_HANDLE_CRC = 0x00,
      /** Parity bits in the network layer of ISO14443-A are by default generated and validated in the PN53X chip. This is a very convenient feature. On certain times though it is useful to get full control of the transmitted data. The proprietary MIFARE Classic protocol uses for example custom (encrypted) parity bits. For interoperability it is required to be completely compatible, including the arbitrary parity bits. When this option is disabled, the functions to communicating bits should be used. */
      NDO_HANDLE_PARITY = 0x01,
      /** This option can be used to enable or disable the electronic field of the NFC device. */
      NDO_ACTIVATE_FIELD = 0x10,
      /** The internal CRYPTO1 co-processor can be used to transmit messages encrypted. This option is automatically activated after a successful MIFARE Classic authentication. */
      NDO_ACTIVATE_CRYPTO1 = 0x11,
      /** The default configuration defines that the PN53X chip will try indefinitely to invite a tag in the field to respond. This could be desired when it is certain a tag will enter the field. On the other hand, when this is uncertain, it will block the application. This option could best be compared to the (NON)BLOCKING option used by (socket)network programming. */
      NDO_INFINITE_SELECT = 0x20,
      /** If this option is enabled, frames that carry less than 4 bits are allowed. According to the standards these frames should normally be handles as invalid frames. */
      NDO_ACCEPT_INVALID_FRAMES = 0x30,
      /** If the NFC device should only listen to frames, it could be useful to let it gather multiple frames in a sequence. They will be stored in the internal FIFO of the PN53X chip. This could be retrieved by using the receive data functions. Note that if the chip runs out of bytes (FIFO = 64 bytes long), it will overwrite the first received frames, so quick retrieving of the received data is desirable. */
      NDO_ACCEPT_MULTIPLE_FRAMES = 0x31,
      /** This option can be used to enable or disable the auto-switching mode to ISO14443-4 is device is compliant */
      NDO_AUTO_ISO14443_4 = 0x40,
      /** Use automatic frames encapsulation and chaining. */
      NDO_EASY_FRAMING = 0x41
    }
    [Flags]
    public enum nfc_modulation_t {
      /** ISO14443-A (NXP MIFARE) http://en.wikipedia.org/wiki/MIFARE */
      NM_ISO14443A_106 = 0x00,
      /** JIS X 6319-4 (Sony Felica) http://en.wikipedia.org/wiki/FeliCa */
      NM_FELICA_212 = 0x01,
      /** JIS X 6319-4 (Sony Felica) http://en.wikipedia.org/wiki/FeliCa */
      NM_FELICA_424 = 0x02,
      /** ISO14443-B http://en.wikipedia.org/wiki/ISO/IEC_14443 */
      NM_ISO14443B_106 = 0x03,
      /** Jewel Topaz (Innovision Research & Development) */
      NM_JEWEL_106 = 0x04,
      /** Active DEP */
      NM_ACTIVE_DEP = 0x05,
      /** Passive DEP */
      NM_PASSIVE_DEP = 0x06
    }
    public enum nfc_target_type_t {
      /** Generic passive 106 kbps (ISO/IEC14443-4A, mifare, DEP) */
      NTT_GENERIC_PASSIVE_106 = 0x00,
      /** Generic passive 212 kbps (FeliCa, DEP) */
      NTT_GENERIC_PASSIVE_212 = 0x01,
      /** Generic passive 424 kbps (FeliCa, DEP) */
      NTT_GENERIC_PASSIVE_424 = 0x02,
      /** Passive 106 kbps ISO/IEC14443-4B */
      NTT_ISO14443B_106 = 0x03,
      /** Innovision Jewel tag */
      NTT_JEWEL_106 = 0x04,
      /** mifare card */
      NTT_MIFARE = 0x10,
      /** FeliCa 212 kbps card */
      NTT_FELICA_212 = 0x11,
      /** FeliCa 424 kbps card */
      NTT_FELICA_424 = 0x12,
      /** Passive 106 kbps ISO/IEC14443-4A */
      NTT_ISO14443A_106 = 0x20,
      /** Passive 106 kbps ISO/IEC14443-4B with TCL flag */
      NTT_ISO14443B_TCL_106 = 0x23,
      /** DEP passive 106 kbps */
      NTT_DEP_PASSIVE_106 = 0x40,
      /** DEP passive 212 kbps */
      NTT_DEP_PASSIVE_212 = 0x41,
      /** DEP passive 424 kbps */
      NTT_DEP_PASSIVE_424 = 0x42,
      /** DEP active 106 kbps */
      NTT_DEP_ACTIVE_106 = 0x80,
      /** DEP active 212 kbps */
      NTT_DEP_ACTIVE_212 = 0x81,
      /** DEP active 424 kbps */
      NTT_DEP_ACTIVE_424 = 0x82
    }
    [DllImport("nfc.dll")]
    private static extern void nfc_list_devices(IntPtr pnddDevices, uint szDevices, ref uint pszDeviceFound);
    [DllImport("nfc.dll")]
    private static extern IntPtr nfc_connect(ref nfc_device_desc_t pndd);
    [DllImport("nfc.dll")]
    private static extern void nfc_disconnect(IntPtr pnd);
    [DllImport("nfc.dll")]
    private static extern bool nfc_initiator_init(IntPtr pnd);
    [DllImport("nfc.dll")]
    private static extern bool nfc_initiator_select_passive_target(IntPtr pnd, nfc_modulation_t nmInitModulation, IntPtr pbtInitData, uint szInitDataLen, IntPtr pti);
    [DllImport("nfc.dll")]
    private static extern bool nfc_initiator_list_passive_targets(IntPtr pnd, nfc_modulation_t nmInitModulation, IntPtr anti, uint szTargets, ref uint pszTargetFound);
    [DllImport("nfc.dll")]
    private static extern bool nfc_configure(IntPtr pnd, nfc_device_option_t ndo, bool bEnable);

    private IntPtr _handle = IntPtr.Zero;

    public nfc_device_desc_t[] list_devices(ref uint uDev) {
      IntPtr ip = Marshal.AllocHGlobal(272 * 16);
      LibNFC.nfc_list_devices(ip, 1, ref uDev);
      nfc_device_desc_t[] device = new nfc_device_desc_t[16];
      for (uint i = 0; i < uDev; i++) {
        device[i] = (nfc_device_desc_t)Marshal.PtrToStructure(new IntPtr(ip.ToInt32() + (272 * i)), typeof(nfc_device_desc_t));
      }
      Marshal.FreeHGlobal(ip);
      return device;
    }

    public nfc_device_t connect(nfc_device_desc_t pndd) {
      disconnect();
      _handle = nfc_connect(ref pndd);
      nfc_device_t ndRet = (nfc_device_t)Marshal.PtrToStructure(_handle, typeof(nfc_device_t));
      return ndRet;
    }

    public void disconnect() {
      if (_handle != IntPtr.Zero) {
        nfc_disconnect(_handle);
      }
    }

    public bool initiator_init() {
      return nfc_initiator_init(_handle);
    }

    public bool configure(nfc_device_option_t ndo, bool enable) {
      return nfc_configure(_handle, ndo, enable);
    }

    public bool initiator_select_passive_target(ref nfc_target_t selected) {
      IntPtr ip = Marshal.AllocHGlobal(89);
      nfc_modulation_t nm = nfc_modulation_t.NM_ISO14443A_106;
      bool bRet = nfc_initiator_select_passive_target(_handle, nm, IntPtr.Zero, 0, ip);
      if (bRet) {
        selected = (nfc_target_t)Marshal.PtrToStructure(ip, typeof(nfc_target_t));
      }
      Marshal.FreeHGlobal(ip);
      return bRet;
    }

    public nfc_target_t[] initiator_list_passive_target_iso14443a(ref uint targets) {
      nfc_target_t[] ant = new nfc_target_t[16];
      IntPtr ip = Marshal.AllocHGlobal(89 * 16);
      nfc_modulation_t nm = nfc_modulation_t.NM_ISO14443A_106 | nfc_modulation_t.NM_JEWEL_106;
      nfc_initiator_list_passive_targets(_handle, nm, ip, 16, ref targets);
      for (uint i = 0; i < targets; i++) {
        ant[i] = (nfc_target_t)Marshal.PtrToStructure(new IntPtr(ip.ToInt32() + (89 * i)), typeof(nfc_target_t));
      }
      Marshal.FreeHGlobal(ip);
      return ant;
    }
  }
}
