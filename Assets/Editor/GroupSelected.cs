using UnityEngine;
using UnityEditor;

public class GroupSelected : EditorWindow
{
	string m_GroupName = "";

	[MenuItem("Hierarchy Tools/Group Selected %g")]
	static void Init()
	{
		GetWindow(typeof(GroupSelected));
	}

	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Label("New group name: ", GUILayout.Width(110));
			m_GroupName = GUILayout.TextField(m_GroupName);
			if (GUILayout.Button("Group", GUILayout.Width(50)))
			{
				Transform grp = (new GameObject(m_GroupName)).transform;
				Transform[] selection = Selection.transforms;

				// Calculate center, put the group there
				Vector3 centroid = Vector3.zero;
				foreach (Transform child in selection)
				{
					if (child.GetComponent<Collider>())
					{
						centroid += child.GetComponent<Collider>().bounds.center;
					}
					else
					{
						centroid += child.position;
					}
				}
				centroid /= (selection.Length);
				grp.position = centroid;

				// Do the actual grouping, and try to keep hierarchy
				Transform commonParent = null;
				foreach (Transform sel in selection)
				{
					if (!commonParent)
					{
						commonParent = sel.parent;
					}
					else if (sel.parent != commonParent)
					{
						commonParent = null;
					}
					sel.parent = grp;
				}
				grp.parent = commonParent;
				Selection.activeTransform = grp;
				Close();
			}
		}
		GUILayout.EndHorizontal();
	}

	[MenuItem("Hierarchy Tools/Unparent selected %u")]
	static void Unparent()
	{
		foreach (Transform sel in Selection.transforms)
		{
			sel.parent = null;
		}
	}

	[MenuItem("Hierarchy Tools/Select parent &p")]
	static void SelectParent()
	{
		if (Selection.activeTransform.parent != null)
		{
			Selection.activeTransform = Selection.activeTransform.parent;
		}
	}
}
