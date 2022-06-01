using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
   public interface IFileManager : ISingleton
   {
      bool SaveFile(string physicalDir, Image img, string fileName, bool overwrite = true);
      bool DeleteFile(string path);

		string GetRandomFileName();
		string GetRandomFileName(string fileExtension);
		string GetRandomFileName(string originalFileName, bool keepExtension);	

	}
}
