public class BTTask_Evade : BT_Node
{
    private BaseBossBehavior boss;
    public BTTask_Evade(BaseBossBehavior boss)
    {
        this.boss = boss;
    }

    protected override NodeResult Execute()
    {
        // Kiểm tra điều kiện để vào trạng thái né tránh (VD: HP thấp)
        if (!boss.ShouldEvade())
        {
            return NodeResult.Failure; // Nếu không cần né tránh, trả về thất bại
        }

        boss.StartEvade(); // Gọi phương thức để bắt đầu né tránh
        return NodeResult.Inprogress; // Trả về trạng thái đang diễn ra
    }

    protected override NodeResult Update()
    {
        if (boss.IsEvadeComplete()) // Kiểm tra xem hành động né tránh đã hoàn thành chưa
        {
            return NodeResult.Success; // Nếu hoàn thành, trả về thành công
        }
        return NodeResult.Inprogress; // Nếu chưa hoàn thành, trả về trạng thái đang diễn ra
    }
}