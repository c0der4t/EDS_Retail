
namespace mainModules
{
    internal class graveyard
    {

       /* private void LoadStockFile(string _pathToStockFile)
        {


            _comments.Clear();
            _stockList.Clear();

            if (File.Exists(_pathToStockFile) == true)
            {
                //Load file
                string currLine = "";
                int lineCount = 0;

                using (StreamReader _stockFlatFile = new StreamReader(_pathToStockFile))
                {
                    while ((currLine = _stockFlatFile.ReadLine()) != null)
                    {
                        lineCount = currLine[0] != '#' ? lineCount += 1 : lineCount = lineCount;

                        if (currLine[0] == '#')
                        {
                            _comments.Add(currLine);
                        }

                        //Checks if we are on line on
                        //If so, skips import
                        //Line one is headings line
                        if ((lineCount > 1) && (currLine[0] != '#'))
                        {
                            string _sku = "";
                            string _qtyonhand = "";
                            string _price = "";
                            string _descr = "";

                            int lengthCurrLine = currLine.Length;
                            int colNumber = 0;


                            while (lengthCurrLine > 0)
                            {
                                colNumber += 1;
                                int indexOfDelimeter = currLine.IndexOf(_flatFileDelimeter) > -1 ? currLine.IndexOf(_flatFileDelimeter) : lengthCurrLine;
                                string currColValue = currLine.Substring(0, indexOfDelimeter);

                                currLine = currLine.IndexOf(_flatFileDelimeter) > -1 ? currLine.Substring(currColValue.Length + _flatFileDelimeter.Length) : "";
                                lengthCurrLine = currLine.Length;

                                switch (colNumber)
                                {
                                    case 1:
                                        _sku = currColValue;
                                        break;
                                    case 2:
                                        _qtyonhand = currColValue;
                                        break;
                                    case 3:
                                        _price = currColValue;
                                        break;
                                    case 4:
                                        _descr = currColValue;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            ImportStocktoGrid(_sku, _qtyonhand, _price, _descr);
                        }


                    }
                }

            }

        }
        */


    }
}
