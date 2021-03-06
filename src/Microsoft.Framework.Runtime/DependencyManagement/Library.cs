// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet;
using System.Collections.Generic;

namespace Microsoft.Framework.Runtime
{
    public class Library : VersionSpec,IEquatable<Library>
    {
        public Library()
        {
            IsMinInclusive = true;
            IsMaxInclusive = true;
        }

        public Library(VersionSpec version)
        {
            if (version == null)
                return;
            IsMinInclusive = version.IsMinInclusive;
            IsMaxInclusive = version.IsMaxInclusive;
            MinVersion = version.MinVersion;
            MaxVersion = version.MaxVersion;
        }

        public string Name { get; set; }

        public SemanticVersion Version { get { return MinVersion; } set { MinVersion = value; } }
        
        public bool IsGacOrFrameworkReference { get; set; }

        public override string ToString()
        {
            var name = IsGacOrFrameworkReference ? "gac/" + Name : Name;
            return name + " " + Version + (Version != null && Version.IsSnapshot ? "-*" : string.Empty);
        }

        public bool Equals(Library other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) &&
                IsMaxInclusive == other.IsMaxInclusive &&
                IsMinInclusive == other.IsMinInclusive &&
                Equals(MinVersion, other.MinVersion) &&
                Equals(MaxVersion, other.MaxVersion) &&
                Equals(IsGacOrFrameworkReference, other.IsGacOrFrameworkReference);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Library)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ 
                    (Version != null ? Version.GetHashCode() : 0) ^
                    (IsGacOrFrameworkReference.GetHashCode());
            }
        }

        public static bool operator ==(Library left, Library right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Library left, Library right)
        {
            return !Equals(left, right);
        }
    }
}
