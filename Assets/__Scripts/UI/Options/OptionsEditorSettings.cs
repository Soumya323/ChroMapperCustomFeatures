﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsEditorSettings : MonoBehaviour
{
    [SerializeField] private Slider editorScaleSlider;
    [SerializeField] private Slider songSpeedSlider;
    [SerializeField] private TextMeshProUGUI songSpeedDisplay;
    [SerializeField] private TMP_InputField autoSaveInterval;
    [SerializeField] private TMP_InputField noteLanes;
    [SerializeField] private Toggle invertControls;
    [SerializeField] private Toggle nodeEditor;
    [SerializeField] private Toggle waveformGenerator;
    [SerializeField] private Toggle countersPlus;
    [SerializeField] private Toggle redNoteDing;
    [SerializeField] private Toggle blueNoteDing;
    [SerializeField] private Toggle bombDing;
    void Start()
    {
        editorScaleSlider.value = EditorScaleController.EditorScale;
        songSpeedSlider.value = (float)OptionsController.Find<SongSpeedController>()?.source.pitch;
        autoSaveInterval.text = OptionsController.Find<AutoSaveController>()?.AutoSaveIntervalMinutes.ToString();
        noteLanes.text = OptionsController.Find<NoteLanesController>()?.NoteLanes.ToString();
        invertControls.isOn = (bool)OptionsController.Find<KeybindsController>()?.InvertNoteKeybinds;
        nodeEditor.isOn = NodeEditorController.IsActive;
        waveformGenerator.isOn = NodeEditorController.IsActive;
        countersPlus.isOn = CountersPlusController.IsActive;
        redNoteDing.isOn = DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_A];
        blueNoteDing.isOn = DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_B];
        bombDing.isOn = DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_BOMB];
    }

    #region Update Editor Variables
    public void UpdateEditorScale(float scale)
    {
        OptionsController.Find<EditorScaleController>()?.UpdateEditorScale(scale);
    }

    public void UpdateSongSpeed(float speed)
    {
        OptionsController.Find<SongSpeedController>()?.UpdateSongSpeed(speed);
        songSpeedDisplay.text = speed * 10 + "%";
    }

    public void ToggleAutoSave(bool enabled)
    {
        OptionsController.Find<AutoSaveController>()?.ToggleAutoSave(enabled);
    }

    public void UpdateAutoSaveInterval(string value)
    {
        OptionsController.Find<AutoSaveController>()?.UpdateAutoSaveInterval(value);
    }

    public void UpdateNoteLanes(string value)
    {
        OptionsController.Find<NoteLanesController>()?.UpdateNoteLanes(value);
    }

    public void UpdateInvertedControls(bool inverted)
    {
        if (OptionsController.Find<KeybindsController>() != null)
            OptionsController.Find<KeybindsController>().InvertNoteKeybinds = inverted;
    }

    public void UpdateNodeEditor(bool enabled)
    {
        NodeEditorController.IsActive = enabled;
    }

    public void UpdateWaveform(bool enabled)
    {
        WaveformGenerator.IsActive = enabled;
    }

    public void UpdateCountersPlus(bool enabled)
    {
        OptionsController.Find<CountersPlusController>()?.ToggleCounters(enabled);
    }

    public void UpdateRedNoteDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_A] = ding;
    }

    public void UpdateBlueNoteDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_B] = ding;
    }

    public void UpdateBombDing(bool ding)
    {
        DingOnNotePassingGrid.NoteTypeToDing[BeatmapNote.NOTE_TYPE_BOMB] = ding;
    }
    #endregion
}