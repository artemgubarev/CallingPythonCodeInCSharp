using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallingPythonCodeInCSharp
{
    public class PythonIterop
    {
        private string pythonDll;
        private string userName;

        public PythonIterop()
        {
            // set python path
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
            pythonDll = $"C:\\Users\\{userName}\\AppData\\Local\\Programs\\Python\\Python38\\python38.dll";
        }

        private void Initialize()
        {
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", pythonDll);
            PythonEngine.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void RunPythonCode(string filePath)
        {
            Initialize();

            using (Py.GIL())
            {
                using (var scope = Py.CreateScope())
                {
                    string pycode = File.ReadAllText(filePath); 
                    scope.Exec(pycode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="paramsNames"></param>
        /// <param name="paramsValues"></param>
        public void RunPythonCodeWithParams(string filePath,string[] paramsNames, object[] paramsValues)
        {
            Initialize();
            int params_count = paramsNames.Length;

            using (Py.GIL())
            {
                using (var scope = Py.CreateScope())
                {
                    string pycode = File.ReadAllText(filePath);

                    for (int i = 0; i < params_count; i++)
                    {
                        scope.Set(paramsNames[i], paramsValues[i].ToPython());
                    }
                    scope.Exec(pycode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="paramsNames"></param>
        /// <param name="paramsValues"></param>
        /// <param name="returningVariableName"></param>
        /// <returns></returns>
        public object RunPythonCodeWithParamsAndReturningValue
            (string filePath, string[] paramsNames, object[] paramsValues, string returningVariableName)
        {
            object result = new object();

            Initialize();
            int params_count = paramsNames.Length;

            using (Py.GIL())
            {
                using (var scope = Py.CreateScope())
                {
                    string pycode = File.ReadAllText(filePath);

                    for (int i = 0; i < params_count; i++)
                    {
                        scope.Set(paramsNames[i], paramsValues[i].ToPython());
                    }
                    scope.Exec(pycode);

                    result = scope.Get<PyObject>(returningVariableName);
                }
            }

            return result;
        }
    }
}
