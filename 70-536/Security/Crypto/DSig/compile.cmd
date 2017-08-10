csc /t:library SignatureHelper.cs
csc /r:SignatureHelper.dll ComputeSignature.cs
csc /r:SignatureHelper.dll VerifySignature.cs