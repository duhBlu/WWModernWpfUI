﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace MUXControlsTestApp.Samples
{
    public partial class Defaults
    {
        public List<string> Data { get; set; }

        public Defaults()
        {
            Data = Enumerable.Range(0, 10000).Select(x => x.ToString()).ToList();
            DataContext = this;
            InitializeComponent();
        }
    }
}
