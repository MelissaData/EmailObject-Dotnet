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
			EncodedString u_License = new EncodedString(License);
			return (mdEmailUnmanaged.mdEmailSetLicenseString(i, u_License.GetPtr()) != 0);
		}

		public void SetPathToEmailFiles(string emailDataFiles) {
			EncodedString u_emailDataFiles = new EncodedString(emailDataFiles);
			mdEmailUnmanaged.mdEmailSetPathToEmailFiles(i, u_emailDataFiles.GetPtr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdEmailUnmanaged.mdEmailInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetInitializeErrorString(i));
		}

		public string GetBuildNumber() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetDatabaseExpirationDate(i));
		}

		public string GetLicenseStringExpirationDate() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetLicenseStringExpirationDate(i));
		}

		public bool VerifyEmail(string email) {
			EncodedString u_email = new EncodedString(email);
			return (mdEmailUnmanaged.mdEmailVerifyEmail(i, u_email.GetPtr()) != 0);
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
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetStatusCode(i));
		}

		public string GetErrorCode() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetErrorCode(i));
		}

		public string GetErrorString() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetErrorString(i));
		}

		public uint GetChangeCode() {
			return mdEmailUnmanaged.mdEmailGetChangeCode(i);
		}

		public string GetResults() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetResults(i));
		}

		public string GetResultCodeDescription(string resultCode, ResultCdDescOpt opt) {
			EncodedString u_resultCode = new EncodedString(resultCode);
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetResultCodeDescription(i, u_resultCode.GetPtr(), (int)opt));
		}

		public string GetResultCodeDescription(string resultCode) {
			EncodedString u_resultCode = new EncodedString(resultCode);
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetResultCodeDescription(i, u_resultCode.GetPtr(), (int)ResultCdDescOpt.ResultCodeDescriptionLong));
		}

		public string GetMailBoxName() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetMailBoxName(i));
		}

		public string GetDomainName() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetDomainName(i));
		}

		public string GetTopLevelDomain() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetTopLevelDomain(i));
		}

		public string GetTopLevelDomainDescription() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetTopLevelDomainDescription(i));
		}

		public string GetEmailAddress() {
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetEmailAddress(i));
		}

		public void SetReserved(string Property, string Value_) {
			EncodedString u_Property = new EncodedString(Property);
			EncodedString u_Value_ = new EncodedString(Value_);
			mdEmailUnmanaged.mdEmailSetReserved(i, u_Property.GetPtr(), u_Value_.GetPtr());
		}

		public string GetReserved(string Property_) {
			EncodedString u_Property_ = new EncodedString(Property_);
			return EncodedString.GetEncodedString(mdEmailUnmanaged.mdEmailGetReserved(i, u_Property_.GetPtr()));
		}

		public void SetCachePath(string cachePath) {
			EncodedString u_cachePath = new EncodedString(cachePath);
			mdEmailUnmanaged.mdEmailSetCachePath(i, u_cachePath.GetPtr());
		}

		public void SetCacheUse(int cacheUse) {
			mdEmailUnmanaged.mdEmailSetCacheUse(i, cacheUse);
		}

		private class EncodedString : IDisposable {
			private IntPtr encodedString = IntPtr.Zero;
			private static Encoding encoding = Encoding.UTF8;

			public EncodedString(string str) {
				if (str == null)
					str = "";
				byte[] buffer = encoding.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				encodedString = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, encodedString, buffer.Length);
			}

			~EncodedString() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(encodedString);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetPtr() {
				return encodedString;
			}

			public static string GetEncodedString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return encoding.GetString(buffer);
			}
		}
	}
}
