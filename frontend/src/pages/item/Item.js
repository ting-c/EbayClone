import React, { useState, useEffect } from 'react'
import './styles.scss'
import itemAPI from '../../api/itemAPI'
import { withRouter } from 'react-router-dom'
import ImagePanel from '../../layouts/imagePanel/ImagePanel'
import SummaryPanel from '../../layouts/summaryPanel/SummaryPanel'

const Item = (props) => {
	
	const id = props.match.params.id;
	
	const [item, setItem] = useState(null);

	useEffect(() => {
		if (!item) {
			fetchItems();
		}
	});

	async function fetchItems(){
		const item = await itemAPI.getByItemId(id);
		setItem(item);
	}  

	return (
		<div className='item-page container-fluid'>
			{	item ? ( 
				<React.Fragment>
					<ImagePanel imageUrls={item.imageUrl} />
					<SummaryPanel item={item} />
				</React.Fragment>
			) : null
			} 
		</div>
	)
}

export default withRouter(Item);
