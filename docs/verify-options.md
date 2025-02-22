<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /docs/mdsource/verify-options.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# Verify Options


## AutoVerify

In some scenarios it makes sense to auto-accept any changes as part of a given test run. For example:

 * Keeping a text representation of a Database schema in a `.verified.sql` file (see [Verify.SqlServer](https://github.com/VerifyTests/Verify.SqlServer)).

This can be done using `AutoVerify()`:

<!-- snippet: AutoVerify -->
<a id='snippet-autoverify'></a>
```cs
var settings = new VerifySettings();
settings.AutoVerify();
```
<sup><a href='/src/Verify.Tests/Snippets/Snippets.cs#L96-L101' title='Snippet source file'>snippet source</a> | <a href='#snippet-autoverify' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Note that auto accepted changes in `.verified.` files remain visible in source control tooling.


### OnHandlers

 * `OnVerify` takes two actions that are called before and after each verification.
 * `OnFirstVerify` is called when there is no verified file.
 * `OnVerifyMismatch` is called when a received file does not match the existing verified file.

<!-- snippet: OnHandlers -->
<a id='snippet-onhandlers'></a>
```cs
public Task OnHandlersSample()
{
    VerifierSettings.OnVerify(
        before: () => { Debug.WriteLine("before"); },
        after: () => { Debug.WriteLine("after"); });
    VerifierSettings.OnFirstVerify(
        receivedFile =>
        {
            Debug.WriteLine(receivedFile);
            return Task.CompletedTask;
        });
    VerifierSettings.OnVerifyMismatch(
        (filePair, message) =>
        {
            Debug.WriteLine(filePair.Received);
            Debug.WriteLine(filePair.Verified);
            Debug.WriteLine(message);
            return Task.CompletedTask;
        });
    return Verifier.Verify("value");
}
```
<sup><a href='/src/Verify.Tests/Snippets/Snippets.cs#L9-L33' title='Snippet source file'>snippet source</a> | <a href='#snippet-onhandlers' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->
