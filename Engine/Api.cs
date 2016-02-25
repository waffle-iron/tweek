﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Context;
using Engine.Keys;
using Engine.Rules;
using LanguageExt;

namespace Engine
{
    public interface IEngine
    {
        Task<Dictionary<ConfigurationPath, ConfigurationValue>> Calculate(
            ConfigurationPath path,
            HashSet<Identity> identities);
    }

    public class Engine : IEngine
    {
        private readonly GetContextByIdentity _getContext;
        private readonly RulesRetriever _rules;
        private readonly PathTraversal _pathTraversal;

        public Engine(GetContextByIdentity getContext,
            PathTraversal pathTraversal,
            RulesRetriever rules)
        {
            _getContext = getContext;
            _rules = rules;
            _pathTraversal = pathTraversal;
        }

        public Task<Dictionary<ConfigurationPath, ConfigurationValue>> Calculate(ConfigurationPath path, HashSet<Identity> identities)
        {
            return _.CalculateImpl(path, _pathTraversal, identities, _getContext, _rules);
        }
    }
}