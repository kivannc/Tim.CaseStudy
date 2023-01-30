
var array1 = new[] { 1, 2, 3, 4, 5 };
var array2 = new[] { 5, 4, 3, 2, 1 };
var array3 = new[] { 1, 3, 5, 2, 4 };
var array4 = new[] { 1 };
var array5 = Array.Empty<int>();

var sortedArray = Sort(array1);
Console.WriteLine(string.Join(",", sortedArray));
sortedArray = Sort(array1);
Console.WriteLine(string.Join(",", sortedArray));
sortedArray = Sort(array2);
Console.WriteLine(string.Join(",", sortedArray));
sortedArray = Sort(array3);
Console.WriteLine(string.Join(",", sortedArray));
sortedArray = Sort(array4);
Console.WriteLine(string.Join(",", sortedArray));
sortedArray = Sort(array5);
Console.WriteLine(string.Join(",", sortedArray));


//BubbleSort
//Space Complexity: O(1)
//Time Complexity: O(n^2)

static int[] Sort(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        var swapped = false;
        for (int j = 0; j < array.Length - 1; j++)
        {
            
            if (array[j] > array[j + 1])
            {
                swapped = true;
                var temp = array[j + 1];
                array[j + 1] = array[j];
                array[j] = temp;
            }
        }
        if (!swapped)
        {
            break;
        }
    }
    return array;
}