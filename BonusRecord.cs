/*********************************
Mã sinh viên: 202419021
Họ tên: Phạm Đình Vũ
Lớp: 20261-174373-LT HDT
*********************************/
namespace LabW04_PayrollSystem;

public class BonusRecord
{
    public double Amount { get; }
    public string Reason { get; }

    public BonusRecord(double amount, string reason)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Khoản thưởng phải lớn hơn 0!");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do thưởng không được để trống!", nameof(reason));

        Amount = amount;
        Reason = reason;
    }

    public override string ToString() => $"+{Amount:N0} VND ({Reason})";
}