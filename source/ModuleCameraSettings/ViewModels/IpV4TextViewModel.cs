using ModuleCameraSettings.Infrastructures;
using System.Collections.Generic;
using System.Linq;

namespace ModuleCameraSettings.ViewModels
{
    public class IpV4TextViewModel : BindableBase
    {
        /// <summary>
        /// The first octet of the IP address.
        /// </summary>
        public string Part1
        {
            get => _part1;
            set => SetProperty(ref _part1, value);
        }
        private string _part1 = default!;

        /// <summary>
        /// The second octet of the IP address.
        /// </summary>
        public string Part2
        {
            get => _part2;
            set => SetProperty(ref _part2, value);
        }
        private string _part2 = default!;

        /// <summary>
        /// The third octet of the IP address.
        /// </summary>
        public string Part3
        {
            get => _part3;
            set => SetProperty(ref _part3, value);
        }
        private string _part3 = default!;

        /// <summary>
        /// The fourth octet of the IP address.
        /// </summary>
        public string Part4
        {
            get => _part4;
            set => SetProperty(ref _part4, value);
        }
        private string _part4 = default!;

        private IEnumerable<byte> Parts
        {
            get
            {
                foreach (string text in new[] { Part1, Part2, Part3, Part4 })
                {
                    if (byte.TryParse(text, out byte value))
                    {
                        yield return value;
                    }
                }
            }
        }

        public bool Valid => Parts.Count() == 4;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public IpV4TextViewModel(string part1 = "", string part2 = "", string part3 = "", string part4 = "")
        {
            Part1 = part1;
            Part2 = part2;
            Part3 = part3;
            Part4 = part4;
        }

        /// <summary>
        /// Returns a string of IP address. (e.g."192.168.0.1")
        /// </summary>
        /// <returns>IP address.</returns>
        public string ToText()
        {
            return string.Join(".", Parts.Select(x => x.ToString()));
        }

        /// <summary>
        /// Returns the population count (number of bits set) of a mask.
        /// </summary>
        /// <returns>the population count.</returns>
        public int PopCount()
        {
            int count = 0;
            foreach (byte part in Parts)
            {
                for (byte x = part; x > 0; x >>= 1)
                {
                    count += (x & 1);
                }
            }
            return count;
        }
    }
}
