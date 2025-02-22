﻿#if !NETSTANDARD2_0
using System.Linq.Expressions;
using VerifyTests;

namespace VerifyNUnit
{
    public partial class VerifyBase
    {
        [Obsolete("Use VerifyTuple")]
        public SettingsTask Verify(
            Expression<Func<ITuple>> expression,
            VerifySettings? settings = null)
        {
            return VerifyTuple(expression, settings);
        }

        public SettingsTask VerifyTuple(
            Expression<Func<ITuple>> target,
            VerifySettings? settings = null)
        {
            settings ??= this.settings;
            return Verifier.VerifyTuple(target, settings);
        }
    }
}
#endif