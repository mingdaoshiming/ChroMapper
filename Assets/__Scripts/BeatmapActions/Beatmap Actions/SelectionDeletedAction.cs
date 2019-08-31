﻿using System.Collections.Generic;
public class SelectionDeletedAction : BeatmapAction
{
    private List<BeatmapObject> deletedData = new List<BeatmapObject>();

    public SelectionDeletedAction(List<BeatmapObjectContainer> selection) : base(null)
    {
        foreach (BeatmapObjectContainer container in selection)
            deletedData.Add(container.objectData);
    }

    public override void Undo(BeatmapActionContainer.BeatmapActionParams param)
    {
        foreach(BeatmapObject data in deletedData)
        {
            BeatmapObjectContainer recovered = null;
            switch (data.beatmapType)
            {
                case BeatmapObject.Type.NOTE:
                    recovered = param.notes.SpawnObject(data);
                    break;
                case BeatmapObject.Type.BOMB:
                    recovered = param.notes.SpawnObject(data);
                    break;
                case BeatmapObject.Type.CUSTOM_NOTE:
                    recovered = param.notes.SpawnObject(data);
                    break;
                case BeatmapObject.Type.OBSTACLE:
                    recovered = param.obstacles.SpawnObject(data);
                    break;
                case BeatmapObject.Type.EVENT:
                    recovered = param.events.SpawnObject(data);
                    break;
                case BeatmapObject.Type.CUSTOM_EVENT:
                    recovered = param.events.SpawnObject(data);
                    break;
            }
            SelectionController.SelectedObjects.Add(recovered);
        }
        SelectionController.RefreshSelectionMaterial(false);
    }

    public override void Redo(BeatmapActionContainer.BeatmapActionParams param)
    {
        param.selection.Delete(false);
    }
}