import { LanLinkAppTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: LanLinkAppTemplatePage;

  beforeEach(() => {
    page = new LanLinkAppTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
