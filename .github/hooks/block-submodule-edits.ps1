$ErrorActionPreference = 'Stop'

function Write-Decision {
    param(
        [Parameter(Mandatory = $true)]
        [ValidateSet('allow', 'deny', 'ask')]
        [string]$Decision,
        [Parameter(Mandatory = $true)]
        [string]$Reason,
        [string]$AdditionalContext
    )

    $output = @{
        hookSpecificOutput = @{
            hookEventName = 'PreToolUse'
            permissionDecision = $Decision
            permissionDecisionReason = $Reason
        }
    }

    if (-not [string]::IsNullOrWhiteSpace($AdditionalContext)) {
        $output.hookSpecificOutput.additionalContext = $AdditionalContext
    }

    $output | ConvertTo-Json -Depth 10 -Compress
}

function Test-ProtectedPathMention {
    param(
        [AllowNull()]
        [string]$Text
    )

    if ([string]::IsNullOrWhiteSpace($Text)) {
        return $false
    }

    return $Text -match '(?i)(^|[\\/])framework-system-ffi-extensions([\\/]|$)'
}

function Convert-ObjectToJsonText {
    param(
        [AllowNull()]
        $Value
    )

    if ($null -eq $Value) {
        return ''
    }

    return ($Value | ConvertTo-Json -Depth 50 -Compress)
}

$rawInput = [Console]::In.ReadToEnd()
if ([string]::IsNullOrWhiteSpace($rawInput)) {
    exit 0
}

$payload = $rawInput | ConvertFrom-Json -Depth 50
$toolName = [string]$payload.tool_name
$toolInput = $payload.tool_input
$toolInputJson = Convert-ObjectToJsonText -Value $toolInput

$mutatingTools = @(
    'apply_patch',
    'create_file',
    'replace_string_in_file',
    'delete_file',
    'rename_file',
    'move_file',
    'editFiles',
    'writeFile',
    'run_in_terminal',
    'send_to_terminal'
)

if ($mutatingTools -contains $toolName -and (Test-ProtectedPathMention -Text $toolInputJson)) {
    Write-Decision -Decision 'deny' -Reason 'Edits to framework-system-ffi-extensions are blocked by workspace policy. Read the submodule for context, but extend framework-dotnet/ instead unless the user explicitly asks for submodule work.' -AdditionalContext 'Protected path matched: framework-system-ffi-extensions/. Use the main library or repo instruction files for downstream integration work.'
    exit 0
}

if ($toolName -in @('apply_patch', 'create_file', 'replace_string_in_file', 'delete_file', 'rename_file', 'move_file', 'editFiles', 'writeFile')) {
    $contextMessage = 'Reminder: framework-system-ffi-extensions/ is read-only by default in this repo. Read it for context, but implement managed follow-up in framework-dotnet/ unless the user explicitly requests submodule edits.'
    Write-Decision -Decision 'allow' -Reason 'Tool allowed.' -AdditionalContext $contextMessage
    exit 0
}

exit 0
