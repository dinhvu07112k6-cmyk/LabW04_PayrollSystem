/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
using System;

namespace LabW04_PayrollSystem;

public class HourlyEmployee : Employee
{
    public double HourlyRate { get; }
    public double WorkedHours { get; private set; }

    // Constructor rút gọn (số giờ làm ban đầu = 0)
    public HourlyEmployee(string employeeId, string fullName, string department, double hourlyRate)
        : this(employeeId, fullName, department, hourlyRate, 0) { }

    // Constructor đầy đủ
    public HourlyEmployee(string employeeId, string fullName, string department, double hourlyRate, double workedHours)
        : base(employeeId, fullName, department)
    {
        if (hourlyRate < 0)
            throw new ArgumentOutOfRangeException(nameof(hourlyRate), "Đơn giá giờ không được âm!");
        if (workedHours < 0 || workedHours > 250)
            throw new ArgumentOutOfRangeException(nameof(workedHours), "Số giờ làm phải từ 0 đến 250 giờ!");

        HourlyRate = hourlyRate;
        WorkedHours = workedHours;
    }

    public void SetWorkedHours(double hours)
    {
        if (hours < 0 || hours > 250)
            throw new ArgumentOutOfRangeException(nameof(hours), "Số giờ làm hợp lệ từ 0 đến 250!");
        WorkedHours = hours;
    }

    public override double CalculateGrossPay()
    {
        double basePay;
        if (WorkedHours <= 160)
        {
            basePay = WorkedHours * HourlyRate;
        }
        else
        {
            basePay = (160 * HourlyRate) + ((WorkedHours - 160) * HourlyRate * 1.5);
        }
        return basePay + MonthlyBonus;
    }

    public override string GetEmployeeType() => "Theo giờ";

    public override void DisplayPayrollInfo()
    {
        Console.WriteLine($"Mã: {EmployeeId,-6} | Họ tên: {FullName,-18} | Phòng: {Department,-12} | Đơn giá: {HourlyRate,9:N0} | Số giờ: {WorkedHours,5}h | Thưởng: {MonthlyBonus,9:N0} | Thu nhập: {CalculateGrossPay(),11:N0} VND");
    }
}