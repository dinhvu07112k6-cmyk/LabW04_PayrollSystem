/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;
using System.Text;

namespace LabW04_PayrollSystem;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=================== CHƯƠNG TRÌNH KIỂM THỬ DỮ LIỆU ĐỀ BÀI ===================");

        // Khởi tạo bảng lương kỳ 2026-09
        Payroll payroll = new("2026-09");

        // 1. E001: Nguyễn Minh An - Đào tạo
        SalariedEmployee e001 = new("E001", "Nguyễn Minh An", "Đào tạo", 15_000_000, 2_000_000);
        e001.AddBonus(1_000_000, "Thưởng cố định đạt KPI");

        // 2. E002: Trần Thu Bình - Hỗ trợ (150h)
        HourlyEmployee e002 = new("E002", "Trần Thu Bình", "Hỗ trợ", 100_000, 150);
        e002.AddBonus(500_000, "Thưởng chuyên cần");

        // 3. E003: Lê Hoàng Chi - Hỗ trợ (170h - có OT)
        HourlyEmployee e003 = new("E003", "Lê Hoàng Chi", "Hỗ trợ", 100_000, 170);

        // 4. E004: Phạm Quốc Dũng - Kinh doanh
        SalesEmployee e004 = new("E004", "Phạm Quốc Dũng", "Kinh doanh", 8_000_000, 200_000_000, 0.05);
        e004.AddBonus(0.02, 50_000_000, "Thưởng 2% vượt mốc doanh số 50tr");

        // Nạp vào bảng lương
        payroll.AddEmployee(e001);
        payroll.AddEmployee(e002);
        payroll.AddEmployee(e003);
        payroll.AddEmployee(e004);

        // In bảng lương kiểm tra
        payroll.DisplayPayroll();

        // =======================================================
        // KIỂM THỬ BIÊN VÀ KIỂM THỬ LỖI (10 TÌNH HUỐNG)
        // =======================================================
        Console.WriteLine("\n=================== C.1. CHẠY 10 TÌNH HUỐNG KIỂM THỬ BIÊN & LỖI ===================");

        RunTestCase(1, "Thêm nhân viên trùng mã E001 vào bảng lương", () => {
            SalariedEmployee duplicate = new("E001", "Người Trùng Mã", "Kế toán", 10_000_000);
            bool res = payroll.AddEmployee(duplicate);
            Console.WriteLine(res ? "Thất bại: Đã cho thêm trùng!" : "Thành công: Đã chặn trùng mã!");
        });

        RunTestCase(2, "Xử lý bảng lương rỗng (Kỳ 2026-10)", () => {
            Payroll emptyPayroll = new("2026-10");
            Console.WriteLine($"Tổng lương: {emptyPayroll.CalculateTotalPayroll():N0} VND");
            Console.WriteLine($"Người cao nhất: {(emptyPayroll.FindHighestPaidEmployee()?.FullName ?? "null (không crash)")}");
            emptyPayroll.DisplayPayroll();
        });

        RunTestCase(3, "Nhân viên làm đúng 160h (Biên ngưỡng làm thêm)", () => {
            HourlyEmployee border160 = new("E_B160", "Trần Biên 160", "Hỗ trợ", 100_000, 160);
            Console.WriteLine($"Thu nhập (160h x 100.000): {border160.CalculateGrossPay():N0} VND (Kỳ vọng: 16.000.000)");
        });

        RunTestCase(4, "Nhân viên làm 161h (Bắt đầu vượt ngưỡng 1h OT)", () => {
            HourlyEmployee border161 = new("E_B161", "Trần Biên 161", "Hỗ trợ", 100_000, 161);
            Console.WriteLine($"Thu nhập (160h thường + 1h x 1.5): {border161.CalculateGrossPay():N0} VND (Kỳ vọng: 16.150.000)");
        });

        RunTestCase(5, "Số giờ làm âm (< 0) của HourlyEmployee", () => {
            _ = new HourlyEmployee("E_ERR1", "Lỗi Giờ Âm", "IT", 100_000, -5);
        });

        RunTestCase(6, "Số giờ làm vượt quá 250 giờ (> 250)", () => {
            _ = new HourlyEmployee("E_ERR2", "Lỗi Giờ Vượt Quá", "IT", 100_000, 251);
        });

        RunTestCase(7, "Tỷ lệ hoa hồng Sales vượt mức 0.3 (> 30%)", () => {
            _ = new SalesEmployee("E_ERR3", "Lỗi Hoa Hồng", "Sale", 5_000_000, 100_000_000, 0.35);
        });

        RunTestCase(8, "Tỷ lệ thưởng vượt quá 0.5 (> 50%) trong AddBonus", () => {
            e001.AddBonus(0.6, 10_000_000, "Vượt quá 50%");
        });

        RunTestCase(9, "Khoản thưởng bằng 0 hoặc âm trong AddBonus", () => {
            e002.AddBonus(-100_000, "Thưởng âm");
        });

        RunTestCase(10, "Mã nhân sự hoặc họ tên để trống", () => {
            _ = new SalariedEmployee("   ", "", "Kỹ thuật", 10_000_000);
        });

        Console.WriteLine("\nNhấn Enter để thoát...");
        Console.ReadLine();
    }

    static void RunTestCase(int testNumber, string description, Action testAction)
    {
        Console.WriteLine($"\n[Test Case {testNumber}] {description}");
        try
        {
            testAction();
            Console.WriteLine("-> Trạng thái: Hợp lệ / Kiểm tra hoàn tất.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"-> Bắt lỗi thành công: [{ex.GetType().Name}] {ex.Message}");
        }
    }
}