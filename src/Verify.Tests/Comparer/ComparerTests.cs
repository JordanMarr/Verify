﻿using VerifyTests;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class ComparerTests
{
    [Fact]
    public async Task Instance_with_message()
    {
        var settings = new VerifySettings();
        settings.UseStringComparer(CompareWithMessage);
        settings.DisableDiff();
        var exception = await Assert.ThrowsAsync<VerifyException>(() => Verifier.Verify("NotTheText", settings));
        Assert.Contains("theMessage", exception.Message);
    }

    [Fact]
    public async Task Instance()
    {
        var settings = new VerifySettings();
        settings.UseStringComparer(Compare);
        await Verifier.Verify("TheText", settings);
        PrefixUnique.Clear();
        await Verifier.Verify("thetext", settings);
    }
#if(Release)
    [Fact]
    public async Task Static_with_message()
    {
        EmptyFiles.Extensions.AddTextExtension("staticComparerExtMessage");
        VerifierSettings.RegisterStringComparer("staticComparerExtMessage", CompareWithMessage);
        var settings = new VerifySettings();
        settings.UseExtension("staticComparerExtMessage");
        settings.DisableDiff();
        var exception = await Assert.ThrowsAsync<Exception>(() => Verifier.Verify("TheText", settings));
        Assert.Equal(
            @"Results do not match.
Differences:
Received: ComparerTests.Static_with_message.received.staticComparerExtMessage
Verified: ComparerTests.Static_with_message.verified.staticComparerExtMessage
Compare Result:
theMessage".Replace("\r\n", "\n"),
            exception.Message.Trim().Replace("\r\n", "\n").Replace("Use DiffEngineTray to verify files.\n",""));
    }
#endif

    [Fact]
    public async Task Static()
    {
        EmptyFiles.Extensions.AddTextExtension("staticComparerExt");
        VerifierSettings.RegisterStringComparer("staticComparerExt", Compare);
        var settings = new VerifySettings();
        settings.UseExtension("staticComparerExt");
        await Verifier.Verify("TheText", settings);
        PrefixUnique.Clear();
        await Verifier.Verify("thetext", settings);
    }

    static Task<CompareResult> Compare(string received, string verified, IReadOnlyDictionary<string, object> context)
    {
        return Task.FromResult(new CompareResult(string.Equals(received, received, StringComparison.OrdinalIgnoreCase)));
    }

    static Task<CompareResult> CompareWithMessage(string stream, string received, IReadOnlyDictionary<string, object> readOnlyDictionary)
    {
        return Task.FromResult(CompareResult.NotEqual("theMessage"));
    }
}