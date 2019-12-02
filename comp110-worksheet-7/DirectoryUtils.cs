using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath){
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path){
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory){
            string[] dicElements = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            long totalSize = 0;

            foreach(string file in dicElements)
            {
                totalSize += GetFileSize(file);
            }

            return totalSize;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory){
            string[] dicElements = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            return dicElements.Length;
		}

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory){
            string[] directories = Directory.GetDirectories(directory);
            int depth = 0;

            foreach(string element in directories)
            {
                depth++;
            }
            return depth;
		}

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory){
            string[] dicElements = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
           
            Tuple<string, long> sizeOfFile = new Tuple<string, long>("file", long.MaxValue);

            string smallest = (from file in dicElements let length = GetFileSize(file) where length > 0 orderby length ascending select file).First();
            long minSize = GetFileSize(smallest);

            string minSizePath = smallest;

            sizeOfFile = new Tuple<string, long>(minSizePath, minSize);
            return sizeOfFile;
		}

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory){
            string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            Tuple<string, long> sizeOfFile = new Tuple<string, long>("file", long.MaxValue);

            string largest = (from file in files let length = GetFileSize(file) where length > 0 orderby length descending select file).First();
            long maxSize = GetFileSize(largest);

            string maxSizePath = largest;

            sizeOfFile = new Tuple<string, long>(maxSizePath, maxSize);
            return sizeOfFile;
		}

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size){
            string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            List<string> filesOfSize = new List<string>();

            long sizeFile;

            foreach(string file in files)
            {
                sizeFile = GetFileSize(file);
                if (sizeFile == size)
                {
                    filesOfSize.Add(file);
                }
            }
            return filesOfSize;
		}
	}
}