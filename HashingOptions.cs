using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PasswordHasherApp
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
