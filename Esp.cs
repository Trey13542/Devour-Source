using System.Collections.Generic;
using UnityEngine;

using static DevourMono.Main;

namespace DevourMono
{
    public class Esp : MonoBehaviour
    {
        Material mat;

        public void Start()
        {
            mat = new Material(Shader.Find("Hidden/Internal-Colored"))
            {
                hideFlags = (HideFlags)61
            };

            mat.SetInt("_SrcBlend", 5);
            mat.SetInt("_DstBlend", 10);
            mat.SetInt("_Cull", 0);
            mat.SetInt("_ZWrite", 0);
        }
        public void OnGUI()
        {
            if (Event.current.type != EventType.Repaint) return;

            if (Settings.PlayerEsp)
            {
                foreach (PlayerCharacterBehaviour p in Players)
                {
                    Vector3 w = Cam.WorldToScreenPoint(p.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.cyan;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, p.transform.position)}m] Player");
                        DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(w.x, Screen.height - w.y), Color.green, 2f);
                        DrawAllBones(GetAllBones(p.GetComponent<Animator>()), Color.cyan);
                        Draw3DBox(p.GetComponent<CapsuleCollider>().bounds, Color.cyan);
                    }
                }
            }
            if (Settings.DemonEsp)
            {
                foreach (SurvivalDemonBehaviour d in Demons)
                {
                    Vector3 w = Cam.WorldToScreenPoint(d.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.red;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, d.transform.position)}m] Demons");
                        DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(w.x, Screen.height - w.y), Color.red, 2f);
                        DrawAllBones(GetAllBones(d.GetComponent<Animator>()), Color.red);
                        Draw3DBox(d.GetComponent<CapsuleCollider>().bounds, Color.red);
                    }
                }
            }
            if (Settings.GoatEsp)
            {
                foreach (GoatBehaviour g in Goats)
                {
                    Vector3 w = Cam.WorldToScreenPoint(g.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.green;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, g.transform.position)}m] Goats");
                    }
                }
            }
            if (Settings.ItemEsp)
            {
                foreach (SurvivalInteractable i in Items)
                {
                    Vector3 w = Cam.WorldToScreenPoint(i.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.yellow;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, i.transform.position)}m] {i.prefabName.Replace("Survival", "")}");
                    }
                }
            }
            if (Settings.KeyEsp)
            {
                foreach (KeyBehaviour k in Keys)
                {
                    Vector3 w = Cam.WorldToScreenPoint(k.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.blue;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, k.transform.position)}m] {k.name.Replace("(Clone)","")}");
                    }
                }
            }
            if (Settings.CollectibleEsp)
            {
                foreach (CollectableInteractable c in Collectibles)
                {
                    Vector3 w = Cam.WorldToScreenPoint(c.transform.position);
                    if (w.z > 0)
                    {
                        GUI.color = Color.magenta;
                        GUI.Label(new Rect(w.x, Screen.height - w.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, c.transform.position)}m] {c.name}");
                    }
                }
            }
            if (Settings.RitualEsp)
            {
                Vector3 rw = Cam.WorldToScreenPoint(Ritual.transform.position);
                if (rw.z > 0)
                {
                    GUI.color = Color.cyan;
                    GUI.Label(new Rect(rw.x, Screen.height - rw.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, Ritual.transform.position)}m] Ritual");
                }
            }
            if (Settings.AzazelEsp)
            {
                Vector3 aw = Cam.WorldToScreenPoint(Azazel.transform.position);
                if (aw.z > 0)
                {
                    GUI.color = Color.red;
                    GUI.Label(new Rect(aw.x, Screen.height - aw.y, 250, 100), $"[{(int)Vector3.Distance(Cam.transform.position, Azazel.transform.position)}m] Azazel");
                    DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(aw.x, Screen.height - aw.y), Color.red, 2f);
                    DrawAllBones(GetAllBones(Azazel.GetComponent<Animator>()), Color.red);
                    Draw3DBox(Azazel.GetComponent<CapsuleCollider>().bounds, Color.red);
                }
            }
        }

        void DrawLine(Vector2 start, Vector2 end, Color color, float width)
        {
            Vector2 d = end - start;
            float a = Mathf.Rad2Deg * Mathf.Atan(d.y / d.x);
            if (d.x < 0)
                a += 180f;

            int w = (int)Mathf.Ceil(width / 2);

            GUI.color = color;
            GUIUtility.RotateAroundPivot(a, start);
            GUI.DrawTexture(new Rect(start.x, start.y - w, d.magnitude, width), Texture2D.whiteTexture, ScaleMode.StretchToFill);
            GUIUtility.RotateAroundPivot(-a, start);
        }
        void DrawBox(Vector3 w, Vector3 wH, Color c)
        {
            float h = Mathf.Abs(wH.y - w.y);
            float x = w.x - h * .3f;
            float y = Screen.height - wH.y;

            DrawBox(new Vector2(x - 1f, y - 1f), new Vector2((h / 2f) + 2f, h + 2f), Color.black); // Outside
            DrawBox(new Vector2(x, y), new Vector2(h / 2f, h), c); // Middle
            DrawBox(new Vector2(x + 1f, y + 1f), new Vector2((h / 2f) - 2f, h - 2f), Color.black); // Inside
        }
        void DrawBox(Vector2 pos, Vector2 size, Color color)
        {
            GUI.color = color;

            GUI.DrawTexture(new Rect(pos.x, pos.y, 1, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(pos.x + size.x, pos.y, 1, size.y), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(pos.x, pos.y + size.y, size.x, 1), Texture2D.whiteTexture);
        }
        void Draw3DBox(Bounds b, Color color)
        {
            Vector3[] pts = new Vector3[8];
            pts[0] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[1] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[2] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[3] = Cam.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
            pts[4] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[5] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[6] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[7] = Cam.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

            for (int i = 0; i < pts.Length; i++) pts[i].y = Screen.height - pts[i].y;

            GL.PushMatrix();
            GL.Begin(1);
            mat.SetPass(0);
            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Begin(1);
            mat.SetPass(0);
            GL.Color(color);
            // Top
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);

            // Bottom
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);

            // Sides
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);

            GL.End();
            GL.PopMatrix();

        }
        void DrawCircle(Color Col, Vector2 Center, float Radius)
        {
            GL.PushMatrix();

            mat.SetPass(0);

            GL.Begin(1);
            GL.Color(Col);

            for (float num = 0f; num < 6.28318548f; num += 0.05f)
            {
                GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
                GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
            }

            GL.End();
            GL.PopMatrix();
        }
        void RectFilled(float x, float y, float width, float height)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), Texture2D.whiteTexture);
        }
        public static List<Transform> GetAllBones(Animator a)
        {
            List<Transform> Bones = new List<Transform>
            {
                a.GetBoneTransform(HumanBodyBones.Head), // 0
                a.GetBoneTransform(HumanBodyBones.Neck), // 1
                a.GetBoneTransform(HumanBodyBones.Spine), // 2
                a.GetBoneTransform(HumanBodyBones.Hips), // 3

                a.GetBoneTransform(HumanBodyBones.LeftShoulder), // 4
                a.GetBoneTransform(HumanBodyBones.LeftUpperArm), // 5
                a.GetBoneTransform(HumanBodyBones.LeftLowerArm), // 6
                a.GetBoneTransform(HumanBodyBones.LeftHand), // 7

                a.GetBoneTransform(HumanBodyBones.RightShoulder), // 8
                a.GetBoneTransform(HumanBodyBones.RightUpperArm), // 9
                a.GetBoneTransform(HumanBodyBones.RightLowerArm), // 10
                a.GetBoneTransform(HumanBodyBones.RightHand), // 11

                a.GetBoneTransform(HumanBodyBones.LeftUpperLeg), // 12
                a.GetBoneTransform(HumanBodyBones.LeftLowerLeg), // 13
                a.GetBoneTransform(HumanBodyBones.LeftFoot), // 14

                a.GetBoneTransform(HumanBodyBones.RightUpperLeg), // 15
                a.GetBoneTransform(HumanBodyBones.RightLowerLeg), // 16
                a.GetBoneTransform(HumanBodyBones.RightFoot) // 17
            };

            return Bones;
        }
        void DrawBones(Transform bone1, Transform bone2, Color c)
        {
            Vector3 w1 = Cam.WorldToScreenPoint(bone1.position);
            Vector3 w2 = Cam.WorldToScreenPoint(bone2.position);
            DrawLine(new Vector2(w1.x, Screen.height - w1.y), new Vector2(w2.x, Screen.height - w2.y), c, 2f);
        }
        void DrawAllBones(List<Transform> b, Color c)
        {
            DrawBones(b[0], b[1], c);
            DrawBones(b[1], b[2], c);
            DrawBones(b[2], b[3], c);

            DrawBones(b[1], b[4], c);
            DrawBones(b[4], b[5], c);
            DrawBones(b[5], b[6], c);
            DrawBones(b[6], b[7], c);

            DrawBones(b[1], b[8], c);
            DrawBones(b[8], b[9], c);
            DrawBones(b[9], b[10], c);
            DrawBones(b[10], b[11], c);

            DrawBones(b[3], b[12], c);
            DrawBones(b[12], b[13], c);
            DrawBones(b[13], b[14], c);

            DrawBones(b[3], b[15], c);
            DrawBones(b[15], b[16], c);
            DrawBones(b[16], b[17], c);
        }

    }
}