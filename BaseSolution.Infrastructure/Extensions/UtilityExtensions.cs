using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSolution.Infrastructure.Extensions
{
    public static class UtilityExtensions
    {
        private static Random random = new Random();
        public static string GenerateRandomString(int totalLength)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < totalLength; i++)
            {
                bool isAlphabet = random.Next(2) == 0;

                if (isAlphabet)
                {
                    int index = random.Next(alphabet.Length);
                    sb.Append(alphabet[index]);
                }
                else
                {
                    int index = random.Next(numbers.Length);
                    sb.Append(numbers[index]);
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// Tính tiền phòng
        /// </summary>
        /// <param name="checkIn">thời gian check in thực tế</param>
        /// <param name="checkOut">thời gian check out thực tế</param>
        /// <param name="giaTienMotDem">giá tiền một đêm</param>
        /// <param name="tienDaTra">số tiền đã trả</param>
        /// <returns></returns>
        public static decimal TinhTien(DateTimeOffset checkIn, DateTimeOffset checkOut, decimal giaTienMotDem, decimal tienDaTra)
        {
            // Thời gian tối ưu khi check in và check out
            DateTimeOffset checkInOptimalTime = checkIn.Date.AddHours(14);
            DateTimeOffset checkOutOptimalTime = checkOut.Date.AddHours(7);

            // Kiểm tra nếu check in và check out nằm ngoài khoảng thời gian tối ưu
            bool checkInLech = checkIn < checkInOptimalTime || checkIn > checkInOptimalTime.AddHours(9);
            bool checkOutLech = checkOut < checkOutOptimalTime || checkOut > checkOutOptimalTime.AddHours(4);

            // Tính số ngày lưu trú (tính cả ngày check-in và ngày check-out)
            int soNgayLuTru = (int)Math.Ceiling((checkOut.Date - checkIn.Date).TotalDays);

            // Tính số tiền phòng
            decimal tongTienPhong = soNgayLuTru * giaTienMotDem;

            // Tính số tiền phụ thu cho check in và check out lệch
            decimal tienPhuThu = 0;

            if (checkInLech)
            {
                tienPhuThu += giaTienMotDem * 0.3m;
            }

            if (checkOutLech)
            {
                tienPhuThu += giaTienMotDem * 0.3m;
            }

            // Tính số tiền cần thanh toán
            decimal tienCanThanhToan = tongTienPhong + tienPhuThu - tienDaTra;

            return tienCanThanhToan;
        }
    }
}
