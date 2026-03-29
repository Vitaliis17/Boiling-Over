using System.Collections.Generic;
using System;

public class InputReaderStateMachine
{
    private Dictionary<ActionMapNames, InputReader> _controlActions;
    private ActionMapNames _current;

    public InputReaderStateMachine(InputReader[] readers)
    {
        SetControlActions(readers);

        SetCurrent(ActionMapNames.Player);
    }

    public void ChangeState(ActionMapNames name)
    {
        if (_current == name)
            return;

        _controlActions[_current].Deactivate();

        SetCurrent(name);
    }

    private void SetCurrent(ActionMapNames name)
    {
        _current = name;
        _controlActions[_current].Activate();
    }

    private void SetControlActions(InputReader[] readers)
    {
        _controlActions = new Dictionary<ActionMapNames, InputReader>();

        Array actionMapNames = Enum.GetValues(typeof(ActionMapNames));

        foreach (InputReader reader in readers)
        {
            foreach (ActionMapNames name in actionMapNames)
            {
                if (reader.Name == name.ToString())
                {
                    _controlActions.Add(name, reader);

                    break;
                }
            }
        }
    }
}