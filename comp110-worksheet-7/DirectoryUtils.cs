using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7{

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
            long totalSize = 0;
            // Getting all elements in the directory
            string[] dicElements = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            //for each item in the array, it iterates 
            foreach(string file in dicElements){
                totalSize += GetFileSize(file);
            }
            // Returns the size of array
            return totalSize;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory){
            string[] dicElements = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            return dicElements.Length;
		}

		public static int GetDepth(int curDepth = 0, string directory){
			
			List<int> depthValues = new List<int>();
            
			string[] depthOfDict = Directory.GetDirectories(directory);

			// If the dictionary has subdirectorys, currDepth adds one
			if(depthOfDict.Length > 0){
				curDepth++;
			}
			// Looping through until lenght of depthOfDict has been reached
			for(int i = 0; i < depthOfDict.Length; i++){
				// Adds curDepth values of each subdirectory to a list
				depthValues.Add(GetDepth(directoryDepth[i], curDepth));
			}
			
			for(int i = 0; i < depthValues.Count; i++){
				if (depthValues[i] > curDepth){
					curDepth = depthValues[i];
				}
			}
			return curDepth;
		}

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory){
            string[] dicElements = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
           
            Tuple<string, long> sizeOfFile = new Tuple<string, long>("file", long.MaxValue);
            // assigning smallest, to the file which, ordered by length, is the smallest in dicElelemts
            string smallest = (from file in dicElements let length = GetFileSize(file) where length > 0 orderby length ascending select file).First();
            long minSize = GetFileSize(smallest);

            string minSizePath = smallest;
            //adding the path and size of the smallest file 
            sizeOfFile = new Tuple<string, long>(minSizePath, minSize);
            return sizeOfFile;
		}

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory){
            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            Tuple<string, long> sizeOfFile = new Tuple<string, long>("file", long.MaxValue);
            // assigning largest, to the file which, ordered by length, is the biggest in dicElelemts
            string largest = (from file in files let length = GetFileSize(file) where length > 0 orderby length descending select file).First();
            long maxSize = GetFileSize(largest);

            string maxSizePath = largest;
            //adding the path and size of the largest file 
            sizeOfFile = new Tuple<string, long>(maxSizePath, maxSize);
            return sizeOfFile;
		}

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size){
            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            List<string> filesOfSize = new List<string>();

            long sizeFile;

            foreach(string file in files) {
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