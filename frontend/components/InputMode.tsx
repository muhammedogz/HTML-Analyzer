import BackupTableIcon from '@mui/icons-material/BackupTable';
import ImageSearchIcon from '@mui/icons-material/ImageSearch';
import LoadingButton from '@mui/lab/LoadingButton';
import { Button, IconButton, Stack, Tooltip, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import DiffEditor from 'components/DiffEditor';
import Editor from 'components/Editor';
import Errors from 'components/Errors';
import { useCallback, useEffect, useState } from 'react';
import { getAnalyzeFromHtml, getFixHtmlAll, HtmlAnalyzerResponseType } from 'src/fetchers/htmlAnalyzerFetchers';
function InputMode() {
  const [code, setCode] = useState('<h1>Hello World!</h1>');
  const [htmlAnalyze, setHtmlAnalyze] = useState<HtmlAnalyzerResponseType | null>(null);
  const [fixedHtml, setFixedHtml] = useState('');
  const [loading, setLoading] = useState(false);
  const [loadingFixButton, setLoadingFixButton] = useState(false);
  const [showDiffEditor, setShowDiffEditor] = useState(false);

  const sendHtml = useCallback(async () => {
    try {
      setLoading(true);
      const response = await getAnalyzeFromHtml(code);
      if (response.status === 200) {
        setHtmlAnalyze(response.data);
        setLoading(false);
      }
    } catch (error) {
      console.error(error);
    }
  }, [code]);

  const getFixedHtml = useCallback(async () => {
    try {
      setLoadingFixButton(true);
      const response = await getFixHtmlAll(code);
      if (response.status === 200) {
        setFixedHtml(response.data.html);
        setLoadingFixButton(false);
      }
    } catch (error) {
      console.error(error);
    }
  }, [code]);

  useEffect(() => {
    if (htmlAnalyze && loading) {
      setHtmlAnalyze(null);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [loading]);

  return (
    <Stack gap={3} id="html-editor-stack">
      <Stack gap={3}>
        <AnalyzerPane gap={2}>
          <Typography fontWeight={700} fontSize="20px" pl="10px">
            Enter HTML Input
          </Typography>
          <Editor code={code} setCode={setCode} />
        </AnalyzerPane>
        <AnalyzerPane justifyContent="center" alignItems="center">
          <LoadingButton
            loading={loading}
            color="secondary"
            variant="contained"
            onClick={sendHtml}
            sx={{
              width: '300px',
            }}
          >
            Send
          </LoadingButton>
        </AnalyzerPane>
      </Stack>
      {htmlAnalyze && (
        <Stack gap={3}>
          <Errors htmlAnalyze={htmlAnalyze} setHtmlAnalyze={setHtmlAnalyze} />
          <AnalyzerPane justifyContent="center" alignItems="center" gap={2} flexDirection="row">
            <Tooltip title="Convert your Tables to Proper Divs!">
              <IconButton>
                <BackupTableIcon />
              </IconButton>
            </Tooltip>
            <Tooltip title="This button will fix all possible errors and warnings. But it will not fix all of them since you should fix them manually.">
              <LoadingButton
                loading={loadingFixButton}
                color="secondary"
                variant="contained"
                onClick={getFixedHtml}
                sx={{
                  width: '300px',
                }}
              >
                Fix your HTML!
              </LoadingButton>
            </Tooltip>
            <Tooltip title="Wrap your images with proper divs!">
              <IconButton>
                <ImageSearchIcon />
              </IconButton>
            </Tooltip>
          </AnalyzerPane>
        </Stack>
      )}
      {fixedHtml.length > 0 && (
        <AnalyzerPane>
          <Button
            onClick={() => {
              setShowDiffEditor(!showDiffEditor);
            }}
          >
            <Typography>{showDiffEditor ? 'Hide Fixed HTML content' : 'Show Fixed HTML'}</Typography>
          </Button>
        </AnalyzerPane>
      )}
      {showDiffEditor && (
        <AnalyzerPane>
          <DiffEditor code={code} setCode={setCode} otherCode={fixedHtml} setOtherCode={setFixedHtml} initialReadOnly />
        </AnalyzerPane>
      )}
    </Stack>
  );
}

export default InputMode;
