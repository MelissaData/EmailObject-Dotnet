using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace MelissaData {
	public class mdEmail : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdEmailUnmanaged {
			[DllImport("mdEmail", EntryPoint = "mdEmailCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailCreate();
			[DllImport("mdEmail", EntryPoint = "mdEmailDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailDestroy(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdEmailSetLicenseString(IntPtr i, IntPtr License);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetPathToEmailFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetPathToEmailFiles(IntPtr i, IntPtr emailDataFiles);
			[DllImport("mdEmail", EntryPoint = "mdEmailInitializeDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdEmailInitializeDataFiles(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetInitializeErrorString(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetBuildNumber(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetDatabaseDate(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetDatabaseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetDatabaseExpirationDate(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetLicenseStringExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetLicenseStringExpirationDate(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailVerifyEmail", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdEmailVerifyEmail(IntPtr i, IntPtr email);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetCorrectSyntax", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetCorrectSyntax(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetUpdateDomain", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetUpdateDomain(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetDatabaseLookup", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetDatabaseLookup(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetFuzzyLookup", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetFuzzyLookup(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetWSLookup", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetWSLookup(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetWSMailboxLookup", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetWSMailboxLookup(IntPtr i, Int32 mailboxLookupmode);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetMXLookup", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetMXLookup(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetStandardizeCasing", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetStandardizeCasing(IntPtr i, Int32 p1);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetStatusCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetStatusCode(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetErrorCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetErrorCode(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetErrorString(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetChangeCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern UInt32 mdEmailGetChangeCode(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetResults", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetResults(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetResultCodeDescription", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetResultCodeDescription(IntPtr i, IntPtr resultCode, Int32 opt);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetMailBoxName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetMailBoxName(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetDomainName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetDomainName(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetTopLevelDomain", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetTopLevelDomain(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetTopLevelDomainDescription", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetTopLevelDomainDescription(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetEmailAddress", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetEmailAddress(IntPtr i);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetReserved(IntPtr i, IntPtr Property, IntPtr Value_);
			[DllImport("mdEmail", EntryPoint = "mdEmailGetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdEmailGetReserved(IntPtr i, IntPtr Property_);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetCachePath", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetCachePath(IntPtr i, IntPtr cachePath);
			[DllImport("mdEmail", EntryPoint = "mdEmailSetCacheUse", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdEmailSetCacheUse(IntPtr i, Int32 cacheUse);
		}

		public mdEmail() {
			i = mdEmailUnmanaged.mdEmailCreate();
		}

		~mdEmail() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdEmailUnmanaged.mdEmailDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public bool SetLicenseString(string License) {
			Utf8String u_License = new Utf8String(License);
			return (mdEmailUnmanaged.mdEmailSetLicenseString(i, u_License.GetUtf8Ptr()) != 0);
		}

		public void SetPathToEmailFiles(string emailDataFiles) {
			Utf8String u_emailDataFiles = new Utf8String(emailDataFiles);
			mdEmailUnmanaged.mdEmailSetPathToEmailFiles(i, u_emailDataFiles.GetUtf8Ptr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdEmailUnmanaged.mdEmailInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetInitializeErrorString(i));
		}

		public string GetBuildNumber() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetDatabaseExpirationDate(i));
		}

		public string GetLicenseStringExpirationDate() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetLicenseStringExpirationDate(i));
		}

		public bool VerifyEmail(string email) {
			Utf8String u_email = new Utf8String(email);
			return (mdEmailUnmanaged.mdEmailVerifyEmail(i, u_email.GetUtf8Ptr()) != 0);
		}

		public void SetCorrectSyntax(bool p1) {
			mdEmailUnmanaged.mdEmailSetCorrectSyntax(i, (p1 ? 1 : 0));
		}

		public void SetUpdateDomain(bool p1) {
			mdEmailUnmanaged.mdEmailSetUpdateDomain(i, (p1 ? 1 : 0));
		}

		public void SetDatabaseLookup(bool p1) {
			mdEmailUnmanaged.mdEmailSetDatabaseLookup(i, (p1 ? 1 : 0));
		}

		public void SetFuzzyLookup(bool p1) {
			mdEmailUnmanaged.mdEmailSetFuzzyLookup(i, (p1 ? 1 : 0));
		}

		public void SetWSLookup(bool p1) {
			mdEmailUnmanaged.mdEmailSetWSLookup(i, (p1 ? 1 : 0));
		}

		public void SetWSMailboxLookup(MailboxLookupMode mailboxLookupmode) {
			mdEmailUnmanaged.mdEmailSetWSMailboxLookup(i, (int)mailboxLookupmode);
		}

		public void SetMXLookup(bool p1) {
			mdEmailUnmanaged.mdEmailSetMXLookup(i, (p1 ? 1 : 0));
		}

		public void SetStandardizeCasing(bool p1) {
			mdEmailUnmanaged.mdEmailSetStandardizeCasing(i, (p1 ? 1 : 0));
		}

		public string GetStatusCode() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetStatusCode(i));
		}

		public string GetErrorCode() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetErrorCode(i));
		}

		public string GetErrorString() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetErrorString(i));
		}

		public uint GetChangeCode() {
			return mdEmailUnmanaged.mdEmailGetChangeCode(i);
		}

		public string GetResults() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetResults(i));
		}

		public string GetResultCodeDescription(string resultCode, ResultCdDescOpt opt) {
			Utf8String u_resultCode = new Utf8String(resultCode);
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetResultCodeDescription(i, u_resultCode.GetUtf8Ptr(), (int)opt));
		}

		public string GetResultCodeDescription(string resultCode) {
			Utf8String u_resultCode = new Utf8String(resultCode);
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetResultCodeDescription(i, u_resultCode.GetUtf8Ptr(), (int)ResultCdDescOpt.ResultCodeDescriptionLong));
		}

		public string GetMailBoxName() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetMailBoxName(i));
		}

		public string GetDomainName() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetDomainName(i));
		}

		public string GetTopLevelDomain() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetTopLevelDomain(i));
		}

		public string GetTopLevelDomainDescription() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetTopLevelDomainDescription(i));
		}

		public string GetEmailAddress() {
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetEmailAddress(i));
		}

		public void SetReserved(string Property, string Value_) {
			Utf8String u_Property = new Utf8String(Property);
			Utf8String u_Value_ = new Utf8String(Value_);
			mdEmailUnmanaged.mdEmailSetReserved(i, u_Property.GetUtf8Ptr(), u_Value_.GetUtf8Ptr());
		}

		public string GetReserved(string Property_) {
			Utf8String u_Property_ = new Utf8String(Property_);
			return Utf8String.GetUnicodeString(mdEmailUnmanaged.mdEmailGetReserved(i, u_Property_.GetUtf8Ptr()));
		}

		public void SetCachePath(string cachePath) {
			Utf8String u_cachePath = new Utf8String(cachePath);
			mdEmailUnmanaged.mdEmailSetCachePath(i, u_cachePath.GetUtf8Ptr());
		}

		public void SetCacheUse(int cacheUse) {
			mdEmailUnmanaged.mdEmailSetCacheUse(i, cacheUse);
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}
