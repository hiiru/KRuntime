// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using Microsoft.Framework.FunctionalTestUtils;
using Microsoft.Framework.Runtime;

namespace Microsoft.Framework.PackageManager
{
    public static class KpmTestUtils
    {
        public static int ExecKpm(string krePath, string subcommand, string arguments,
            IDictionary<string, string> environment = null, string workingDir = null)
        {
            string program, commandLine;
            if (PlatformHelper.IsMono)
            {
                program = Path.Combine(krePath, "bin", "kpm");
                commandLine = string.Format("{0} {1}", subcommand, arguments);
            }
            else
            {
                program = "cmd";
                var kpmCmdPath = Path.Combine(krePath, "bin", "kpm.cmd");
                commandLine = string.Format("/C {0} {1} {2}", kpmCmdPath, subcommand, arguments);
            }

            string stdOut, stdErr;
            return TestUtils.Exec(program, commandLine, out stdOut, out stdErr, environment, workingDir);
        }
    }
}
