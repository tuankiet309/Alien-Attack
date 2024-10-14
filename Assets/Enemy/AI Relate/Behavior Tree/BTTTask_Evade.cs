namespace Preb.Over_All.AI_Relate.Behavior_Tree
{
    public class BTTask_Evade : BT_Node
    {
        BaseBossBehavior boss;
        public BTTask_Evade(BaseBossBehavior boss)
        {
            this.boss = boss;
        }

        protected override NodeResult Execute()
        {
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

}