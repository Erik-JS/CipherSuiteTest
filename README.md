# CipherSuiteTest

This tool demonstrates the use of API functions from bcrypt.dll which can be used for listing, adding and removing cipher suites used in secure communication protocols (SSL/TLS).

Code in CipherSuiteControl.cs was created based on original C++ code on Microsoft's docs: https://docs.microsoft.com/en-us/windows/win32/secauthn/prioritizing-schannel-cipher-suites<br />
All nice enums with all possible values for use with the functions, CRYPT_CONTEXT_FUNCTIONS struct, use of Marshal left and right... this thing is as 'complete' as I could make it.

Specifically created for helping with the use of [ME3 Private Server Emulator](https://github.com/PrivateServerEmulator/ME3PSE).

Reason: since Windows 10 v1709, Microsoft has "disabled by default" support for specific cipher suites used in SSL3 communication between Mass Effect 3 and PSE. This action only interferes with the server, not the game.

Usage:
* **Get list of cipher suites**: lists all currently enabled cipher suites
* **Add**: enables `TLS_RSA_WITH_RC4_128_SHA` and `TLS_RSA_WITH_RC4_128_MD5`
* **Remove**: disables `TLS_RSA_WITH_RC4_128_SHA` and `TLS_RSA_WITH_RC4_128_MD5`

Like PowerShell's cmdlets `Enable-TlsCipherSuite` and `Disable-TlsCipherSuite`, this tool requires administrator rights for `BCryptAddContextFunction` and `BCryptRemoveContextFunction` to work properly.