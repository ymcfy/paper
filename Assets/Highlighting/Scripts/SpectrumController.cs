using UnityEngine;
public class SpectrumController : HighlightingController
{
    public float speed = 0;//速度
    private readonly int period = 1530;
    private float counter = 0f;
    private readonly float scale = 0.03f;
    protected override void AfterUpdate()
    {
        base.AfterUpdate();
        int val = (int)counter;
        Color col = new Color(GetColorValue(1020, val), GetColorValue(0, val), GetColorValue(510, val), 1f);//边框颜色
        ho.ConstantOnImmediate(col);
        //暂时注释  杨民  20220418
        //counter += Time.deltaTime * speed;
        //counter %= period;
        ////通过键盘滑轮控制物体的缩放
        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftAlt))
        //{
        //    transform.localScale += new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftAlt))
        //{
        //    transform.localScale -= new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);
        //}
        //else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftAlt))
        //{
        //    transform.position += new Vector3(0, 3f * scale, 0);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftAlt))
        //{
        //    transform.position -= new Vector3(0, 3f * scale, 0);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.position -= new Vector3(3f * scale, 0, 0);
        //}
        //else if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.position += new Vector3(3f * scale, 0, 0);
        //}
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.position += new Vector3(0, 0, 3f * scale);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.position -= new Vector3(0, 0, 3f * scale);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0,1,0, Space.World);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(-1,0,0,Space.World);
        //}
    }
    // Some color spectrum magic
    private float GetColorValue(int offset, int x)
    {
        int o = 0;
        x = (x - offset) % period;
        if (x < 0)
        {
            x += period;
        }
        if (x < 255)
        {
            o = x;
        }
        if (x >= 255 && x < 765)
        {
            o = 255;
        }
        if (x >= 765 && x < 1020)
        {
            o = 1020 - x;
        }
        return o / 255f;
    }
}
