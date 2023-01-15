import { LoadingButton } from '@mui/lab';
import { Button, Stack, TextField, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import DiffEditor from 'components/DiffEditor';
import Editor from 'components/Editor';
import Errors from 'components/Errors';
import { useCallback, useState } from 'react';
import { getAnalyzeFromUrl, getFixHtmlAll, HtmlAnalyzerResponseType } from 'src/fetchers/htmlAnalyzerFetchers';

const URLRegexp = /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/;

const URLMode = () => {
  const [url, setUrl] = useState('');
  const [showEditor, setShowEditor] = useState(false);
  const [loading, setLoading] = useState(false);
  const [code, setCode] = useState('');
  const [fixedHtml, setFixedHtml] = useState('');
  const [htmlAnalyze, setHtmlAnalyze] = useState<HtmlAnalyzerResponseType | null>(null);
  const [loadingFixButton, setLoadingFixButton] = useState(false);
  const [showDiffEditor, setShowDiffEditor] = useState(false);

  const getHtml = useCallback(async () => {
    try {
      setLoading(true);
      const response = await getAnalyzeFromUrl(url);
      if (response.status === 200) {
        setHtmlAnalyze(response.data);
        setCode(response.data.html);
        setLoading(false);
      }
    } catch (error) {
      console.error(error);
    }
  }, [url]);

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

  return (
    <Stack gap={4} minWidth={{ xs: '85vw', md: '800px' }}>
      <Stack gap={2}>
        <AnalyzerPane>
          <TextField label="URL" onChange={(e) => setUrl(e.target.value)} />
        </AnalyzerPane>
        <AnalyzerPane justifyContent="center" alignItems="center">
          <LoadingButton
            disabled={!URLRegexp.test(url)}
            loading={loading}
            onClick={getHtml}
            color="secondary"
            variant="contained"
            sx={{
              width: '300px',
            }}
          >
            Get HTML
          </LoadingButton>
        </AnalyzerPane>
      </Stack>
      {htmlAnalyze && (
        <Stack gap={3}>
          <Errors htmlAnalyze={htmlAnalyze} setHtmlAnalyze={setHtmlAnalyze} />
          <AnalyzerPane justifyContent="center" alignItems="center">
            <Typography>
              {htmlAnalyze.html.length} characters, {htmlAnalyze.html.split(' ').length} words,
            </Typography>
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
          </AnalyzerPane>
        </Stack>
      )}
      {code.length > 0 && (
        <AnalyzerPane>
          <Button
            onClick={() => {
              setShowEditor(!showEditor);
            }}
          >
            <Typography>{showEditor ? 'Hide fetched HTML text' : 'Show fetched HTML'}</Typography>
          </Button>
        </AnalyzerPane>
      )}
      {showEditor && (
        <AnalyzerPane>
          <Editor code={code} setCode={setCode} initialReadOnly />
        </AnalyzerPane>
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
};

export default URLMode;
